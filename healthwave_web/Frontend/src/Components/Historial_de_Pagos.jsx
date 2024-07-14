import React, { useEffect, useState } from "react";
import {
  getFacturas,
  getFacturasByDate,
} from "../services/firestoreService.js";
import "../Styles/Historial_de_Pagos.css";
import Navbar from "./Patient/Principal/Navbar.jsx"; // Asegúrate de que la ruta del Navbar sea correcta

const FacturaHistory = () => {
  const [facturas, setFacturas] = useState([]);
  const [day, setDay] = useState("");
  const [month, setMonth] = useState("");
  const [year, setYear] = useState("");

  useEffect(() => {
    const fetchFacturas = async () => {
      try {
        const facturasList = await getFacturas();
        setFacturas(facturasList);
      } catch (error) {
        console.error("Error fetching facturas: ", error);
      }
    };
    fetchFacturas();
  }, []);

  const formatMonto = (monto) => {
    if (typeof monto === "number") {
      return monto.toFixed(2);
    }
    return "0.00";
  };

  const formatFecha = (fecha) => {
    if (fecha && fecha.seconds) {
      return new Date(fecha.seconds * 1000).toLocaleDateString();
    }
    return "Fecha no disponible";
  };

  const handleFilter = async () => {
    try {
      const filteredFacturas = await getFacturasByDate(day, month, year);
      setFacturas(filteredFacturas);
    } catch (error) {
      console.error("Error fetching filtered facturas: ", error);
    }
  };

  return (
    <div className="wrapper">
      <Navbar />
      <div className="factura-history">
        <h2>Historial de Facturas</h2>
        <div className="filter">
          <label className="label">Filtrar por fecha:</label>
          <div className="filter-inputs">
            <input
              type="text"
              placeholder="DD"
              className="input"
              value={day}
              onChange={(e) => setDay(e.target.value)}
            />
            <input
              type="text"
              placeholder="MM"
              className="input"
              value={month}
              onChange={(e) => setMonth(e.target.value)}
            />
            <input
              type="text"
              placeholder="YYYY"
              className="input"
              value={year}
              onChange={(e) => setYear(e.target.value)}
            />
            <button onClick={handleFilter} className="filter-button">
              Filtrar
            </button>
          </div>
        </div>
        <div className="facturas-list">
          {facturas.length > 0 ? (
            facturas.map((factura) => (
              <div key={factura.id} className="factura-card">
                <div className="factura-details">
                  <h3>Factura</h3>
                  <p>Código de Factura: {factura.FacturaCodigo}</p>
                  <p>Fecha: {formatFecha(factura.Fecha)}</p>
                  <p>Monto Total: {formatMonto(factura.MontoTotal)} RD$</p>
                  <p>
                    Monto Subtotal: {formatMonto(factura.MontoSubtotal)} RD$
                  </p>
                  <p>RNC: {factura.RNC || "No especificado"}</p>
                  <p>
                    Método de Pago:{" "}
                    {factura.CodigoMetodoDePago || "No especificado"}
                  </p>
                  <p>
                    Código de Paciente:{" "}
                    {factura.CodigoPaciente || "No especificado"}
                  </p>
                  <p>ID Ingreso: {factura.IDIngreso || "No especificado"}</p>
                  <p>ID Cuenta: {factura.IDCuenta || "No especificado"}</p>
                  <p>
                    Código de Consulta:{" "}
                    {factura.ConsultaCodigo || "No especificado"}
                  </p>
                </div>
              </div>
            ))
          ) : (
            <div className="no-facturas">
              <h3>No se han realizado facturas</h3>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default FacturaHistory;
