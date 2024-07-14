// src/scripts/addNotifications.js
import { addNotification } from "../services/firestoreService";

const notifications = [
  {
    tipo: "Afección",
    mensaje: "Nueva afección agregada: Gripe",
  },
  {
    tipo: "Aseguradora",
    mensaje: "Nueva aseguradora agregada: Seguros Vida",
  },
  {
    tipo: "Autorización",
    mensaje: "Nueva autorización emitida con monto: 1500.00",
  },
  {
    tipo: "Consulta",
    mensaje: "Nueva consulta creada con el estado: Pendiente",
  },
  {
    tipo: "Ingreso",
    mensaje: "Nuevo ingreso registrado con costo de estancia: 2000.00",
  },
];

const addNotificationsToFirestore = async () => {
  for (const notification of notifications) {
    await addNotification(notification);
  }
};

addNotificationsToFirestore()
  .then(() => {
    console.log("Notificaciones agregadas a Firestore");
  })
  .catch((error) => {
    console.error("Error al agregar notificaciones: ", error);
  });
