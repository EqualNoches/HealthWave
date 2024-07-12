import React from "react";
import "../../../Styles/Pagina_principal/ProfileCard.css";
import profileImage from "/mi perfil.jpg";
import "../../../Styles/Pagina_principal/card.css";

const ProfileCard = () => {
  return (
    <div className="card">
      <img src={profileImage} alt="Perfil" />
      <div className="card-content">
        <h3> Mi Perfil </h3>
        <button className="card-button">Ver perfil</button>
      </div>
    </div>
  );
};

export default ProfileCard;
