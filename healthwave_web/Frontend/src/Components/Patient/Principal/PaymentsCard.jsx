import React from "react";
import "../../../Styles/Pagina_principal/PaymentsCard.css";
import paymentsImage from "/gestion de productos.jpg";
import "../../../Styles/Pagina_principal/card.css";

const PaymentsCard = () => {
  return (
    <div className="card">
      <img src={paymentsImage} alt="Cuentas por Pagar" />
      <div className="card-content">
        <h3>Cuentas por Pagar</h3>
        <button className="card-button">Ver</button>
      </div>
    </div>
  );
};

export default PaymentsCard;
