import React, { useEffect, useState } from "react";
import { getNotificationsForUser } from "../../../services/firestoreService";
import("../../../Styles/Pagina_principal/Notifications.css");

const Notifications = ({ codigoDocumento, tipoDocumento }) => {
  const [notifications, setNotifications] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const data = await getNotificationsForUser(
        codigoDocumento,
        tipoDocumento
      );
      setNotifications(data);
    };
    fetchData();
  }, [codigoDocumento, tipoDocumento]);

  return (
    <div className="notifications">
      <h3>Notificaciones</h3>
      {notifications.length === 0 ? (
        <p>No hay notificaciones para mostrar.</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>Tipo</th>
              <th>Mensaje</th>
            </tr>
          </thead>
          <tbody>
            {notifications.map((notification) => (
              <tr key={notification.id}>
                <td>{notification.tipo}</td>
                <td>{notification.mensaje}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default Notifications;
