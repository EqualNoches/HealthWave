import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import NotFound from "@/views/NotFound.vue";
import store from "@/store";
import { push } from "notivue"
import api from '@/utilities/api';
import UsuariosView from '@/views/UsuariosView.vue';
import ConsultoriosView from '@/views/ConsultoriosView.vue';
import AseguradorasView from '@/views/AseguradorasView.vue';
import ConsultasView  from '@/views/ConsultasView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Inicio',
      icon: 'pi pi-home',
      component: HomeView,
      meta: {
        authenticated: false,
      }
    },
    {
      path: '/Login',
      name: 'Login',
      component: LoginView,
      meta: {
        authenticated: false,
      }
    },
    {
      path: '/Usuarios',
      name: 'Usuarios',
      component: UsuariosView,
      meta: {
        authenticated: true,
      }
    },
    {
      path: '/Consultorios',
      name: 'Consultorios',
      component: ConsultoriosView,
      meta: {
        authenticated: true,
      }
    },
    {
      path: '/Aseguradoras',
      name: 'Aseguradoras',
      component: AseguradorasView,
      meta: {
        authenticated: true,
      }
    },
    {
      path: '/Consultas',
      name: 'Consultas',
      component: ConsultasView,
      meta: {
        authenticated: true,
      }
    },
    { path: "/:pathMatch(.*)*", name: "404", component: NotFound, authenticated: false, administrador: true },
  ]
})

router.beforeEach(async (to, from) => {
  if (to == from) {
    return false;
  }

  if (localStorage.getItem("DarkMode") == null) {
    if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
      localStorage.setItem("DarkMode", true);
    } else {
      localStorage.setItem("DarkMode", false);
    }
  }

  store.commit('setIsDarkMode', localStorage.getItem("DarkMode") == "true");
  document.documentElement.classList.toggle('p-dark', localStorage.getItem("DarkMode") == "true");

  let authenticated = await isAuthenticated(to);
  document.title = `${to.name} - HealthWave Administración`;

  if (!authenticated && to.name != "Login") {
    localStorage.removeItem("token");
    localStorage.removeItem('user');
    localStorage.removeItem('sessionExpireTime');
    store.commit('setScreenLocked', false);
    document.title = "Iniciar sesión - HealthWave Administración";
    return { name: "Login" };
  }
});

async function isAuthenticated() {
  if (localStorage.getItem("token") == null) return false;
  
  try {
    store.commit('setUser', { nombreUsuario: localStorage.getItem('user'), nombrePerfil: "Administrador", nombres: "Ronnie Ismael", apellidos: "Difo de Leon"})
    return true;
    
    // const response = await api.get("/api/Account");

    // if (response.data) {
    //   store.commit('setUser', response.data.usuario);
    //   return true;
    // }
    // else {
    //   return false;
    // }
  } catch (error) {
    return false;
  }
}

export default router