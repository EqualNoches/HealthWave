import React from "react";
import { Link } from "react-router-dom";
import "../Styles/Crear_usuario.css";
import background from "/img_hospital.jpg";

const Register = () => {
  return (
    <div className="register-background">
      <div className="register-container">
        <div className="register-box">
          <h2>Registro</h2>
          <form>
            <div className="input-group">
              <label htmlFor="first-name">Nombre</label>
              <input
                type="text"
                id="first-name"
                name="first-name"
                placeholder="Nombre"
              />
            </div>
            <div className="input-group">
              <label htmlFor="last-name">Apellido</label>
              <input
                type="text"
                id="last-name"
                name="last-name"
                placeholder="Apellido"
              />
            </div>
            <div className="input-group">
              <label htmlFor="email">Email</label>
              <input
                type="email"
                id="email"
                name="email"
                placeholder="example@gmail.com"
              />
            </div>
            <div className="input-group">
              <label htmlFor="password">Contraseña</label>
              <input
                type="password"
                id="password"
                name="password"
                placeholder="********"
              />
            </div>
            <div className="input-group">
              <label htmlFor="confirm-password">Reescribe la contraseña</label>
              <input
                type="password"
                id="confirm-password"
                name="confirm-password"
                placeholder="********"
              />
            </div>
            <button type="submit" className="Button-Register">
              Regístrate
            </button>
          </form>
          <div className="login-redirect">
            <span>
              ¿Deseas iniciar sesión? <Link to="/">Atrás</Link>
            </span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Register;
