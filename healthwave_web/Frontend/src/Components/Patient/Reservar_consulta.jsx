import React, { useEffect, useState } from "react";
import { getServicios } from "../../services/firestoreService";
import "../../Styles/Reservar_consulta.css";
import Navbar from "../Patient/Principal/Navbar"; // Asegúrate de que la ruta del Navbar sea correcta

const Servicios = () => {
  const [servicios, setServicios] = useState([]);

  useEffect(() => {
    const fetchServicios = async () => {
      try {
        const serviciosList = await getServicios();
        setServicios(serviciosList);
      } catch (error) {
        console.error("Error fetching servicios: ", error);
      }
    };
    fetchServicios();
  }, []);

  return (
    <div className="wrapper">
      <Navbar />
      <div className="container">
        <h1>Nuestros Servicios</h1>
        <div className="row">
          {servicios.length > 0 ? (
            servicios.map((servicio) => (
              <div key={servicio.id} className="service">
                <i className="fas fa-concierge-bell"></i>
                <h2>{servicio.Nombre}</h2>
                <p>{servicio.Descripcion}</p>
                <p>Costo: RD${servicio.Costo.toFixed(2)}</p>
              </div>
            ))
          ) : (
            <div className="no-servicios">
              <h3>No hay servicios disponibles</h3>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default Servicios;
