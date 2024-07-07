<template>
  <Dialog v-model:visible="$store.state.screenLocked" v-if="$route.name !== 'Login'" modal :closable="false" :style="{ width: '25rem', zIndex: 499 }">
    <template #header>
        <div class="inline-flex align-items-center justify-content-center gap-2 text-xl">
            <i class="fas fa-lock" />
            <span class="font-bold white-space-nowrap">Pantalla bloqueada</span>
        </div>
    </template>
    <span class="p-text-secondary block mb-5">La sesión concluyó, ingrese nuevamente</span>
    <form>
      <div class="flex align-items-center gap-3 mb-3">
        <label for="username" class="font-semibold w-6rem">Usuario</label>
        <InputText id="username" class="flex-auto" disabled v-model="credentials.Username" autocomplete="off" />
      </div>
      <div class="flex align-items-center gap-3 mb-5">
        <label for="password" class="font-semibold w-6rem">Contraseña</label>
        <Password type="password" autocomplete="current-password" id="password" :invalid="invalid.Password" size="small" variant="filled" v-model="credentials.Password" toggleMask :feedback="false" />
        
      </div>
      <div class="flex justify-content-between gap-2">
        <Button severity="secondary" label="Cerrar Sesión" @click="LogOut" />
        <Button type="submit" primary label="Iniciar Sesión" @click="LogIn" />
      </div>
    </form>
  </Dialog>

</template>

<script>
import { push } from 'notivue'
import api from '@/utilities/api.js'

export default {
  data() {
    return {     
      isBlocked: false,
      invalid: {
        Password: false,
      },
      credentials: {
        Username: localStorage.getItem('user'),
        Password: "",
      },
    };
  },
  mounted() {
    this.startSessionCheckInterval();
  },
  beforeUnmount() {
    clearInterval(this.sessionCheckInterval);
  },
  methods: {
    startSessionCheckInterval() {
      this.sessionCheckInterval = setInterval(() => {
        this.checkSessionExpiration();
      }, 60000);
      this.checkSessionExpiration();
    },
    checkSessionExpiration() {
      const session = localStorage.getItem('token');
      if (session) {
        const expirationTime = localStorage.getItem('sessionExpireTime');
        const currentTime = new Date().getTime();
        if (currentTime > expirationTime) {
          this.sessionExpired = true;
          localStorage.removeItem('token');
          localStorage.removeItem('sessionExpireTime');
          clearInterval(this.sessionCheckInterval);
          this.$store.commit('setScreenLocked', true);
        } else {
          this.sessionExpired = false;
        }
      } else {
        this.sessionExpired = true;
        localStorage.removeItem('token');
        localStorage.removeItem('sessionExpireTime');
        clearInterval(this.sessionCheckInterval);
        this.$store.commit('setScreenLocked', false);
        this.$router.push('/Login');
      }
    },
    LogOut() {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      this.$store.commit('setUser', {});
      this.$store.commit('setScreenLocked', false);
      this.$router.push('/Login');
    },
    async LogIn(event) {
      event.preventDefault();
      if (this.credentials.Password === "") {
        this.invalid.Password = true;
        push.warning("Por favor, rellene el campo de contraseña")
      }
      else {
        this.invalid.Password = false;

        // 
        localStorage.setItem("token", "token");
        localStorage.setItem("user", this.credentials.Username);

        this.$store.commit('setUser', { nombreUsuario: this.credentials.Username, nombrePerfil: "Administrador", nombres: "Nombre", apellidos: "Apellido" });

        this.$store.commit('setScreenLocked', false);
        const sessionExpireTime = new Date().getTime() + (30 * 60 * 1000);
        localStorage.setItem('sessionExpireTime', sessionExpireTime);

        this.$router.push('/');
        push.success("Ha iniciado sesión exitosamente");

        // const response = await api.post('/api/Account', {
        //     UserName: this.credentials.Username,
        //     Password: this.credentials.Password
        // });

        // if (response.data.success) {
        //     const token = response.data.token;
        //     localStorage.setItem("token", token);
        //     localStorage.setItem("user", response.data.data.usuario.nombreUsuario);

        //     push.success(response.data.message);

        //     const { usuario, vistas } = response.data.data;
            
        //     this.$store.commit('setUser', usuario);
        //     this.$store.commit('setViews', vistas);
            
        //     this.$store.commit('setScreenLocked', false);

        //     const sessionExpireTime = new Date().getTime() + (30 * 60 * 1000);
        //     localStorage.setItem('sessionExpireTime', sessionExpireTime);
        // }
        // else push.warning(response.data.message);
      }
    },
  },
}
</script>