import { db, auth } from "./firebase";
import { collection, addDoc } from "firebase/firestore";
import { createUserWithEmailAndPassword } from "firebase/auth";

export const registerUser = async (
  email,
  password,
  nombre,
  apellido,
  genero,
  fechaNacimiento,
  telefono,
  direccion,
  rol,
  tipoDocumento,
  codigoDocumento
) => {
  try {
    // Registrar al usuario
    const userCredential = await createUserWithEmailAndPassword(
      auth,
      email,
      password
    );
    const user = userCredential.user;

    // Crear un documento en la colección 'PerfilUsuario'
    await addDoc(collection(db, "PerfilUsuario"), {
      userId: user.uid,
      Nombre: nombre,
      Apellido: apellido,
      Genero: genero,
      FechaNacimiento: fechaNacimiento,
      Telefono: telefono,
      Correo: email,
      Direccion: direccion,
      Rol: rol,
      TipoDocumento: tipoDocumento,
      CodigoDocumento: codigoDocumento,
      NumLicenciaMedica: null, // Si aplica
    });

    console.log("Usuario y perfil de usuario creados con éxito");
  } catch (error) {
    console.error(
      "Error registrando al usuario y creando el perfil de usuario: ",
      error
    );
    throw error;
  }
};
