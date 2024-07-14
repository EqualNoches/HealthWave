// src/services/firestoreService.js
import { db } from "../firebaseConfig";
import { collection, getDocs } from "firebase/firestore";

export const getNotificationsForUser = async (
  codigoDocumento,
  tipoDocumento
) => {
  try {
    const q = query(
      collection(db, "Notifications"),
      where("CodigoDocumento", "==", codigoDocumento),
      where("TipoDocumento", "==", tipoDocumento)
    );
    const querySnapshot = await getDocs(q);
    const notifications = [];
    querySnapshot.forEach((doc) => {
      notifications.push({ id: doc.id, ...doc.data() });
    });
    return notifications;
  } catch (error) {
    console.error("Error fetching notifications: ", error);
    return [];
  }
};

// Función para obtener el perfil de usuario
export const getUserProfile = async (userId) => {
  try {
    // Referencia al documento del perfil de usuario usando CodigoDocumento como ID
    const docRef = doc(collection(db, "PerfilUsuario"), userId);

    // Obtener el documento
    const docSnap = await getDoc(docRef);

    if (docSnap.exists()) {
      // Obtener los datos del documento
      const data = docSnap.data();

      // Convertir los campos de fecha a un formato legible
      if (data.FechaNacimiento) {
        data.FechaNacimiento =
          data.FechaNacimiento.toDate().toLocaleDateString();
      }

      console.log("User Profile:", data); // Log para ver los datos obtenidos
      return data;
    } else {
      console.error("No such document!");
      return null;
    }
  } catch (error) {
    console.error("Error fetching user profile:", error);
    return null;
  }
};

export const getAppointmentsForUser = async (
  codigoDocumento,
  tipoDocumento
) => {
  try {
    const q = query(
      collection(db, "Appointments"),
      where("CodigoDocumento", "==", codigoDocumento),
      where("TipoDocumento", "==", tipoDocumento)
    );
    const querySnapshot = await getDocs(q);
    const appointments = [];
    querySnapshot.forEach((doc) => {
      const data = doc.data();
      if (data.hora) {
        data.hora = data.hora.toDate().toLocaleTimeString();
      }
      appointments.push({ id: doc.id, ...data });
    });
    return appointments;
  } catch (error) {
    console.error("Error fetching appointments: ", error);
    return [];
  }
};

export const getPayments = async () => {
  try {
    const querySnapshot = await getDocs(collection(db, "Payments"));
    const payments = [];
    querySnapshot.forEach((doc) => {
      const data = doc.data();
      // Convertir los campos de fecha a un formato legible si es necesario
      if (data.fecha) {
        data.fecha = data.fecha.toDate().toLocaleDateString();
      }
      payments.push({ id: doc.id, ...data });
    });
    console.log("Fetched Payments:", payments); // Log to see the fetched data
    return payments;
  } catch (error) {
    console.error("Error fetching payments:", error);
    return [];
  }
};

// Función para obtener todas las facturas
export const getFacturas = async () => {
  try {
    const querySnapshot = await getDocs(collection(db, "Factura"));
    const facturasList = querySnapshot.docs.map((doc) => ({
      id: doc.id,
      ...doc.data(),
    }));
    return facturasList;
  } catch (error) {
    console.error("Error fetching facturas: ", error);
    throw error;
  }
};

// Función para obtener facturas filtradas por fecha
export const getFacturasByDate = async (day, month, year) => {
  try {
    const startDate = new Date(year, month - 1, day);
    const endDate = new Date(year, month - 1, day + 1);

    const q = query(
      collection(db, "Factura"),
      where("Fecha", ">=", startDate),
      where("Fecha", "<", endDate)
    );

    const querySnapshot = await getDocs(q);
    const facturasList = querySnapshot.docs.map((doc) => ({
      id: doc.id,
      ...doc.data(),
    }));

    return facturasList;
  } catch (error) {
    console.error("Error fetching facturas by date: ", error);
    throw error;
  }
};

export const getServicios = async () => {
  const serviciosCollection = collection(db, "Servicios");
  const serviciosSnapshot = await getDocs(serviciosCollection);
  const serviciosList = serviciosSnapshot.docs.map((doc) => ({
    id: doc.id,
    ...doc.data(),
  }));
  return serviciosList;
};
