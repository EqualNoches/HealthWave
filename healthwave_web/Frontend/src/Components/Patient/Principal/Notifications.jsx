import React from 'react';
import '../../../Styles/Pagina_principal/Notifications.css';

const Notifications = () => {
  return (
    <div className="notifications">
      <h3>Notificaciones y Alertas</h3>
      <table>
        <thead>
          <tr>
            <th>Tipo</th>
            <th>Mensaje</th>
          </tr>
        </thead>
        <tbody>
          {/* Aquí irían las filas de notificaciones */}
        </tbody>
      </table>
    </div>
  );
};

export default Notifications;