import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import "../Styles/Crear_usuario.css";
import { registerUser } from "../services/authService";
import background from "/img_hospital.jpg";

const Register = () => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [genero, setGenero] = useState("M"); // Default 'M'
  const [fechaNacimiento, setFechaNacimiento] = useState("");
  const [telefono, setTelefono] = useState("");
  const [direccion, setDireccion] = useState("");
  const [tipoDocumento, setTipoDocumento] = useState("I"); // Default 'I'
  const [codigoDocumento, setCodigoDocumento] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const rol = "C"; // Siempre Cliente

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (password !== confirmPassword) {
      setError("Las contraseñas no coinciden");
      return;
    }

    try {
      await registerUser(
        email,
        password,
        firstName,
        lastName,
        genero,
        fechaNacimiento,
        telefono,
        direccion,
        rol, // Siempre Cliente
        tipoDocumento,
        codigoDocumento
      );
      navigate("/homepage");
    } catch (error) {
      setError("Error al registrar usuario: " + error.message);
    }
  };

  return (
    <div className="register-background">
      <div className="register-container">
        <div className="register-box">
          <h2>Registro</h2>
          <form className="form-container" onSubmit={handleSubmit}>
            <div className="input-group">
              <label htmlFor="first-name">Nombre</label>
              <input
                type="text"
                id="first-name"
                name="first-name"
                placeholder="Nombre"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
              />
            </div>
            <div className="input-group">
              <label htmlFor="last-name">Apellido</label>
              <input
                type="text"
                id="last-name"
                name="last-name"
                placeholder="Apellido"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
              />
            </div>
            <div className="input-group">
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
            <div className="input-group">
              <label htmlFor="password">Contraseña</label>
              <input
                type="password"
                id="password"
                name="password"
                placeholder="Password should be at least 6 characters"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>
            <div className="input-group">
              <label htmlFor="confirm-password">Reescribe la contraseña</label>
              <input
                type="password"
                id="confirm-password"
                name="confirm-password"
                placeholder="Reenter the password"
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
              />
            </div>
            <div className="input-group">
              <label htmlFor="genero">Género</label>
              <select
                id="genero"
                name="genero"
                value={genero}
                onChange={(e) => setGenero(e.target.value)}
              >
                <option value="M">Masculino</option>
                <option value="F">Femenino</option>
              </select>
            </div>
            <div className="input-group">
              <label htmlFor="fechaNacimiento">Fecha de Nacimiento</label>
              <input
                type="date"
                id="fechaNacimiento"
                name="fechaNacimiento"
                value={fechaNacimiento}
                onChange={(e) => setFechaNacimiento(e.target.value)}
                required
              />
            </div>
            <div className="input-group">
              <label htmlFor="telefono">Teléfono</label>
              <input
                type="text"
                id="telefono"
                name="telefono"
                value={telefono}
                onChange={(e) => setTelefono(e.target.value)}
              />
            </div>
            <div className="input-group">
              <label htmlFor="direccion">Dirección</label>
              <input
                type="text"
                id="direccion"
                name="direccion"
                value={direccion}
                onChange={(e) => setDireccion(e.target.value)}
              />
            </div>
            <div className="input-group">
              <label htmlFor="tipoDocumento">Tipo de Documento</label>
              <select
                id="tipoDocumento"
                name="tipoDocumento"
                value={tipoDocumento}
                onChange={(e) => setTipoDocumento(e.target.value)}
              >
                <option value="I">Identificación</option>
                <option value="P">Pasaporte</option>
              </select>
            </div>
            <div className="input-group">
              <label htmlFor="codigoDocumento">Código del Documento</label>
              <input
                type="text"
                id="codigoDocumento"
                name="codigoDocumento"
                value={codigoDocumento}
                onChange={(e) => setCodigoDocumento(e.target.value)}
                required
              />
            </div>
            {error && <p className="error-message">{error}</p>}
            <button type="submit" className="Register-Button">
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
