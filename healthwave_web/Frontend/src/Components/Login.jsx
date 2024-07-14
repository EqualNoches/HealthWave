import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import "../Styles/Login.css";
import { loginUser } from "../services/authService";
import background from "/img_hospital.jpg";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await loginUser(email, password);
      navigate("/homepage");
    } catch (error) {
      setError("Email o contraseña incorrectos");
    }
  };

  return (
    <div className="login-background">
      <div className="login-container">
        <div className="login-box">
          <h2>Inicio Sesión</h2>
          <form onSubmit={handleSubmit}>
            <div className="input-group1">
              <label htmlFor="email">Email</label>
              <input
                type="email"
                id="email"
                name="email"
                placeholder="example@gmail.com"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>
            <div className="input-group1">
              <label htmlFor="password">Contraseña</label>
              <input
                type="password"
                id="password"
                name="password"
                placeholder="********"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>
            {error && <p className="error-message">{error}</p>}
            <div className="forgot-password">
              <Link to="/register">
                <a href="#">Forget password?</a>
              </Link>
            </div>
            <button type="submit" className="Button-Login">
              Iniciar Sesión
            </button>
          </form>
          <div className="register">
            <span>
              No tienes una cuenta? <Link to="/register">Regístrate</Link>
            </span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
