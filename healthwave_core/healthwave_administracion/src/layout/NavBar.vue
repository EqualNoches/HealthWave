<template>
  <header class="flex align-items-center justify-content-between px-4 lg:hidden py-2">
    <SideBarMobile />
    <div class="flex-grow-1">
      <div class="flex flex-1 justify-content-end align-items-center gap-4">
        <div class="relative">
          <Button severity="secondary" text class="flex justify-content-between text-color-secondary" @click="toggleProfileMenu">
            <div class="flex align-items-center gap-1">
              <Avatar icon="pi pi-user" class="mr-2" size="normal" />
              <span class="flex flex-column text-left justify-content-start align-content-center" v-if="$store.state.user != {}">
                  <span class="font-medium text-sm">{{ $store.state.user.nombreUsuario }}</span>
                  <span class="font-light text-xs">Administrador</span>
              </span>
              <span class="flex flex-column text-left justify-content-start align-content-center" v-else>
                <Skeleton width="6rem" height="17px" class="mb-1"></Skeleton>
                <Skeleton width="6rem" height="13px"></Skeleton>  
              </span>
            </div>
          </Button>
        </div>
      </div>
    </div>
    <Menu ref="profile_menu" id="profile_menu" class="p-1" :model="profileMenuItems" :popup="true" :pt="{
      list: { class: 'flex flex-1 gap-1' },
    }" />
  </header>
</template>

<script>
import SideBarMobile from './SideBarMobile.vue';

export default {
  name: "NavBar",
  components: {
    SideBarMobile,
  },
  data() {
    return {
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
  props: {},
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
  },
};
</script>