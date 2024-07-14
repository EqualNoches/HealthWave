import React, { useEffect, useState } from "react";
import { getAppointmentsForUser } from "../../../services/firestoreService"; // Asegúrate de importar correctamente
import "../../../Styles/Pagina_principal/Appointments.css"; // Asegúrate de importar el archivo CSS

const Appointments = ({ codigoDocumento, tipoDocumento }) => {
  const [appointments, setAppointments] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const data = await getAppointmentsForUser(codigoDocumento, tipoDocumento);
      setAppointments(data);
    };
    fetchData();
  }, [codigoDocumento, tipoDocumento]);

  return (
    <div className="appointments">
      <h3>Próximas Citas</h3>
      {appointments.length === 0 ? (
        <p>No hay citas para mostrar.</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>Fecha</th>
              <th>Hora</th>
              <th>Consultorio</th>
            </tr>
          </thead>
          <tbody>
            {appointments.map((appointment) => (
              <tr key={appointment.id}>
                <td>{appointment.fecha}</td>
                <td>{appointment.hora}</td>
                <td>{appointment.consultorio}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default Appointments;
