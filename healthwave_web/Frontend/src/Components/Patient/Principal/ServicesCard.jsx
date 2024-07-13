import React from "react";
import { Link } from "react-router-dom";
import "../../../Styles/Pagina_principal/ServicesCard.css";
import serviceImage from "/gestion de servicios.jpg";
import "../../../Styles/Pagina_principal/card.css";

const ServicesCard = () => {
  return (
    <div className="card">
      <img src={serviceImage} alt="Servicios" />
      <div className="card-content">
        <h3>Servicios</h3>
        <Link to="/">
          <button className="card-button">Ver Servicios</button>
        </Link>
      </div>
    </div>
  );
};

export default ServicesCard;
