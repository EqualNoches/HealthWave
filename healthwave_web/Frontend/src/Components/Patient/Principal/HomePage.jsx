import React from "react";
import Navbar from "./Navbar.jsx";
import Appointments from "./Appointment.jsx";
import Notifications from "./Notifications.jsx";
import ServicesCard from "./ServicesCard.jsx";
import ProfileCard from "./ProfileCard.jsx";
import TransactionsCard from "./TransactionsCard.jsx";
import PaymentsCard from "./PaymentsCard.jsx";
import "../../../Styles/Pagina_principal/HomePage.css";

const HomePage = () => {
  return (
    <div className="home-page">
      <Navbar />
      <div className="main-content">
        <div className="top-section">
          <Appointments />
          <Notifications />
          <PaymentsCard />
        </div>
        <div className="bottom-section">
          <ServicesCard />
          <ProfileCard />
          <TransactionsCard />
        </div>
      </div>
    </div>
  );
};

export default HomePage;
