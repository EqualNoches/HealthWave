import React from "react";
import "../../../Styles/Pagina_principal/TransactionsCard.css";
import transactionImage from "/historial de transacciones.png";
import "../../../Styles/Pagina_principal/card.css";

const TransactionsCard = () => {
  return (
    <div className="card">
      <img
        className="Img-history"
        src={transactionImage}
        alt="Historial de Transacciones"
      />
      <div className="card-content">
        <h3>Historial de Transacciones</h3>
        <button className="card-button">Ver historial</button>
      </div>
    </div>
  );
};

export default TransactionsCard;
