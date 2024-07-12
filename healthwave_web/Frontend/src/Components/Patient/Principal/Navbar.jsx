import React from "react";
import { Link } from "react-router-dom";
import "../../../Styles/Pagina_principal/Navbar.css";

const Navbar = () => {
  return (
    <nav className="navbar">
      <ul className="nav-list">
        <li>
          <Link to="/">Página principal</Link>
        </li>
        <li>
          <Link to="/payments">Pagos</Link>
        </li>
        <li>
          <Link to="/transactions">Transacciones</Link>
        </li>
        <li>
          <Link to="/services">Servicios</Link>
        </li>
        <li>
          <Link to="/profile">Perfil</Link>
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;
