import { useEffect, useState } from "react";

export default function PurchasesDashboard() {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const controller = new AbortController();

    async function loadUsers() {
      try {
        const response = await fetch(
          "/api/users/purchases",
          { signal: controller.signal }
        );

        if (!response.ok) {
          throw new Error("Failed to load users");
        }

        const data = await response.json();
        setUsers(data);
      } catch (err) {
        if (err.name !== "AbortError") {
          setError(err.message || "Something went wrong");
        }
      } finally {
        if (!controller.signal.aborted) {
          setLoading(false);
        }
      }
    }

    loadUsers();

    return () => controller.abort();
  }, []);

  if (loading) return <p>Loading users...</p>;
  if (error) return <p role="alert">{error}</p>;

  return (
    <main>
      <h1>Customer Purchases</h1>
      {users.map((user) => (
        <section key={user.id}>
          <h2>{user.name}</h2>

          {user.purchases.length > 0 ? (
            <ul>
              {user.purchases.map((purchase) => (
                <li key={purchase.id}>
                  {purchase.product} — $
                  {purchase.amount.toFixed(2)}
                </li>
              ))}
            </ul>
          ) : (
            <p>No purchases yet</p>
          )}
        </section>
      ))}
    </main>
  );
}
