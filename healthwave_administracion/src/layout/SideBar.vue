<template>
  <div class=" left-0 w-20rem h-full hidden fixed lg:block">
    <Menu :model="items" class="flex w-full md:w-60 h-full bg-transparent border-none flex-column" :pt="{
      list: { class: 'flex flex-1 px-3 overflow-y-auto	' },
    }">
      <template #start>
        <span class="flex justify-content-center align-items-center gap-3 p-3 w-full">
          <router-link to="/" class="flex justify-content-center align-items-center gap-3 w-full" style="text-decoration: none; color: inherit;">
            <img src="@/assets/logo.png" alt="CGRD"  style="height: 5rem; object-fit: contain" />
          </router-link>
        </span>
        <div class="p-menu-separator mb-1" />
      </template>
      <template #submenulabel="{ item }">
        <span class="text-sm font-semibold text-color-secondary">{{ item.label }}</span>
      </template>
      <template #item="{ item }">
        <SideBarItem :item="item" />
      </template>
      <template #end>
        <div class="p-menu-separator mb-1" />
        <div class="p-3 pt-2">
          <Button severity="secondary" text class="w-full flex justify-content-between text-color-secondary" @click="toggleProfileMenu">
            <div class="flex align-items-center gap-1">
              <Avatar icon="pi pi-user" class="mr-2" size="large" />
              <span class="flex flex-column text-left justify-content-start align-content-center"  v-if="$store.state.user != {}">
                <span class="font-medium">{{ $store.state.user.nombreUsuario }}</span>
                <span class="text-sm font-light">{{ $store.state.user.nombrePerfil}}</span>
              </span>
              <span class="flex flex-column text-left justify-content-start align-content-center" v-else>
                <Skeleton width="9rem" height="20px" class="mb-1"></Skeleton>
                <Skeleton width="6rem" height="15px"></Skeleton>  
              </span>
            </div>
            <i class="fas fa-chevron-up text-xs" />
          </Button>
        </div>
      </template>
    </Menu>
    <Menu ref="profile_menu" id="profile_menu" class="p-1" :model="profileMenuItems" :popup="true" :pt="{
      list: { class: 'flex flex-1 gap-1' },
    }" />
  </div>
</template>

<script>
import { push } from 'notivue'

export default {
  data() {
    return {
      items: [
        {
          label: "Inicio",
          icon: "fas fa-home",
          shortcut: " ",
          path: "/",
        },
        {
          label: "Administración",
          items: [
            {
              label: "Usuarios",
              icon: "fas fa-user",
              shortcut: " ",
              path: "/Usuarios",
            },
            {
              label: "Consultorios",
              icon: "fas fa-hospital",
              shortcut: " ",
              path: "/Consultorios",
            },
            {
              label: "Aseguradoras",
              icon: "fas fa-id-card-clip",
              shortcut: " ",
              path: "/Aseguradoras",
            },
            {
              label: "Metodos de Pago",
              icon: "fas fa-credit-card",
              shortcut: " ",
              path: "/MetodosDePago",
            },
            {
              label: "Productos",
              icon: "fas fa-boxes-stacked",
              shortcut: " ",
              path: "/Productos",
            },
            {
              label: "Cuentas por Cobrar",
              icon: "fas fa-cash-register",
              shortcut: " ",
              path: "/CuentasPorCobrar",
            },
            {
              label: "Consultas",
              icon: "fas fa-file-medical",
              shortcut: " ",
              path: "/Consultas",
            }
          ]
        },
        {
          label: "Otros",
          items: [
            {
              label: "Configuración",
              icon: "fas fa-cog",
              shortcut: " ",
              path: "/Configuracion",
            },
          ],
        },
      ],
      profileMenuItems: [
        {
          label: 'Cambiar tema',
          icon: `${this.$store.state.isDarkMode ? 'pi pi-sun' : 'pi pi-moon'}`,
          command: () => {
            this.$refs.profile_menu.model[0].icon = `${!this.$store.state.isDarkMode ? 'pi pi-sun' : 'pi pi-moon'}`;
            localStorage.setItem('DarkMode', !this.$store.state.isDarkMode);
            this.$store.commit('setIsDarkMode', !this.$store.state.isDarkMode);
            document.documentElement.classList.toggle('p-dark', localStorage.getItem("DarkMode") == "true");
          }
        },
        {
          label: 'Bloquear pantalla',
          icon: 'pi pi-lock',
          command: () => {
            localStorage.removeItem("token");
            localStorage.removeItem('sessionExpireTime');
            this.$store.commit('setScreenLocked', true);
          }
        },
        {
          label: 'Cerrar sesión',
          icon: 'pi pi-sign-out',
          command: () => {
            localStorage.removeItem('user');
            localStorage.removeItem('token');
            localStorage.removeItem('sessionExpireTime');
            this.$store.commit('setUser', {});
            this.$store.commit('setViews', []);
            push.success('Sesión cerrada con éxito');
            this.$router.push('/Login');
          }
        }
      ]
    };
  },
  methods: {
    toggleProfileMenu(event) {
      this.$refs.profile_menu.toggle(event);
    },
    getFirstLastName(Nombres, Apellidos) {
      if (Nombres != null && Apellidos != null) {
        let firstName = Nombres.split(" ");
        firstName = firstName[0];
        let lastName = Apellidos.split(" ");
        lastName = lastName[0];
        return firstName + " " + lastName;
      }
    }
  }
};
</script>
