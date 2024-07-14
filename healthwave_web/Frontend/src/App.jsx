import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Login from "./Components/Login.jsx";
import Register from "./Components/Crear_usuario.jsx";
import HomePage from "./Components/Patient/Principal/HomePage.jsx";
import ProfileView from "./Components/Patient/Revisar_perfil_cuentas_Patient.jsx";
import "./App.css";
import PaymentHistory from "./Components/Historial_de_Pagos.jsx";
import Servicios from "./Components/Patient/Reservar_consulta.jsx";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/register" element={<Register />} />
        <Route path="/homepage" element={<HomePage />} />
        <Route path="/" element={<Login />} />
        <Route path="/profileview" element={<ProfileView />} />
        <Route path="/login" element={<Login />} />
        <Route path="/paymenthistory" element={<PaymentHistory />} />
        <Route path="/services" element={<Servicios />} />
      </Routes>
    </Router>
  );
}

export default App;
