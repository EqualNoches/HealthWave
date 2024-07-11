import React from "react";
import "../Styles/Login.css"; // Asegúrate de crear este archivo CSS para los estilos
import background from "/img_hospital.jpg";

const Login = () => {
  return (
    <div className="login-background">
      <div className="login-container">
        <div className="login-box">
          <h2>Inicio Sesión</h2>
          <form>
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
            <div className="forgot-password">
              <a href="#">Forget password?</a>
            </div>
            <button type="submit">Iniciar Sesión</button>
          </form>
          <div className="register">
            <span>
              No tienes una cuenta? <a href="#">Regístrate</a>
            </span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
