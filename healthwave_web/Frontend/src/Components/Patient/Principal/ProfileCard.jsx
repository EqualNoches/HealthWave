import React from "react";
import { Link } from "react-router-dom";
import "../../../Styles/Pagina_principal/ProfileCard.css";
import profileImage from "/mi perfil.jpg";
import "../../../Styles/Pagina_principal/card.css";

const ProfileCard = () => {
  return (
    <div className="card">
      <img src={profileImage} alt="Perfil" />
      <div className="card-content">
        <h3> Mi Perfil </h3>
        <Link to="/profileview">
          <button className="card-button">Ver perfil</button>
        </Link>
      </div>
    </div>
  );
};

export default ProfileCard;
