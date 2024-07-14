import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { registerUser } from "../../services/authService";
import "../../Styles/Crear_usuario.css";
import background from "/img_hospital.jpg";

const Register = () => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [gender, setGender] = useState("");
  const [birthDate, setBirthDate] = useState("");
  const [phone, setPhone] = useState("");
  const [address, setAddress] = useState("");
  const [docType, setDocType] = useState("");
  const [docCode, setDocCode] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (password !== confirmPassword) {
      setError("Las contraseñas no coinciden");
      return;
    }

    const userProfile = {
      Nombre: firstName,
      Apellido: lastName,
      Genero: gender,
      FechaNacimiento: new Date(birthDate),
      Telefono: phone,
      Correo: email,
      Direccion: address,
      TipoDocumento: docType,
      CodigoDocumento: docCode,
      Rol: "C", // Asumiendo que todos los usuarios registrados son clientes
    };

    try {
      await registerUser(email, password, userProfile);
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
          <form onSubmit={handleSubmit}>
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
                placeholder="********"
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
                placeholder="********"
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
              />
            </div>
            <div className="input-group">
              <label htmlFor="gender">Género</label>
              <input
                type="text"
                id="gender"
                name="gender"
                placeholder="M/F"
                value={gender}
                onChange={(e) => setGender(e.target.value)}
              />
            </div>
            <div className="input-group">
              <label htmlFor="birthDate">Fecha de Nacimiento</label>
              <input
                type="date"
                id="birthDate"
                name="birthDate"
                value={birthDate}
                onChange={(e) => setBirthDate(e.target.value)}
              />
            </div>
            <div className="input-group">
              <label htmlFor="phone">Teléfono</label>
              <input
                type="text"
                id="phone"
                name="phone"
                placeholder="Teléfono"
                value={phone}
                onChange={(e) => setPhone(e.target.value)}
              />
            </div>
            <div className="input-group">
              <label htmlFor="address">Dirección</label>
              <input
                type="text"
                id="address"
                name="address"
                placeholder="Dirección"
                value={address}
                onChange={(e) => setAddress(e.target.value)}
              />
            </div>
            <div className="input-group">
              <label htmlFor="docType">Tipo de Documento</label>
              <input
                type="text"
                id="docType"
                name="docType"
                placeholder="I/P"
                value={docType}
                onChange={(e) => setDocType(e.target.value)}
              />
            </div>
            <div className="input-group">
              <label htmlFor="docCode">Código de Documento</label>
              <input
                type="text"
                id="docCode"
                name="docCode"
                placeholder="Código de Documento"
                value={docCode}
                onChange={(e) => setDocCode(e.target.value)}
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
