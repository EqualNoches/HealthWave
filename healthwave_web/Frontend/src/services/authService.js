// src/services/authService.js
import { auth } from "../firebaseConfig.js";
import { signInWithEmailAndPassword } from "firebase/auth";
import { getAuth, createUserWithEmailAndPassword } from "firebase/auth";
import { doc, setDoc } from "firebase/firestore";
import { db } from "../firebaseConfig.js"; // Asegúrate de importar tu configuración de Firebase correctamente

export const loginUser = async (email, password) => {
  try {
    const userCredential = await signInWithEmailAndPassword(
      auth,
      email,
      password
    );
    return userCredential.user;
  } catch (error) {
    throw error;
  }
};

export const registerUser = async (email, password, userProfile) => {
  const auth = getAuth();

  try {
    const userCredential = await createUserWithEmailAndPassword(
      auth,
      email,
      password
    );
    const user = userCredential.user;

    // Verificar que userProfile es un objeto válido
    console.log("userProfile:", userProfile);

    // Crear el documento del perfil de usuario en Firestore
    const userDoc = doc(db, "PerfilUsuario", user.uid);
    await setDoc(userDoc, userProfile);

    return user;
  } catch (error) {
    throw new Error("Error al registrar usuario: " + error.message);
  }
};
