import React from 'react';
import '../../../Styles/Pagina_principal/Appointments.css';

const Appointments = () => {
  return (
    <div className="appointments">
      <h3>Próximas Citas</h3>
      <table>
        <thead>
          <tr>
            <th>Pacientes</th>
            <th>Hora</th>
            <th>Consultorio</th>
          </tr>
        </thead>
        <tbody>
          {/* Aquí irían las filas de citas */}
        </tbody>
      </table>
    </div>
  );
};

export default Appointments;