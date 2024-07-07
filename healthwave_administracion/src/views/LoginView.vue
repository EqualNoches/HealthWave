<template>
    <div class="w-full h-full flex flex-column justify-content-center align-items-center">
        <img src="@/assets/logo.png" class="my-2 h-8rem sm:h-13rem"/>
        <div class="p-5 max-w-30rem my-2 border-round-lg card">
            <form>
                <div class="text-lg sm:text-2xl font-bold mb-4 text-center">Iniciar Sesión</div>
                <div class="mb-4">
                    <label class="font-semibold mb-2 w-full">👤 Nombre de Usuario <span class="text-danger" v-if="invalid.UserName">*</span></label>
                    <InputText :pt="{root: { autocomplete:'username'} }" type="text" :invalid="invalid.UserName" class="w-full mt-2" variant="filled" v-model="credentials.UserName" />
                </div>
                <div class="mb-4">
                    <label class="font-semibold mb-2 w-full">🔑 Contraseña <span class="text-danger" v-if="invalid.Password">*</span></label>
                    <Password type="password" :inputProps="{autocomplete:'current-password'}" :invalid="invalid.Password" class="w-full mt-2" variant="filled" v-model="credentials.Password" toggleMask :feedback="false" />
                </div>
                <Button type="submit" primary label="Acceder" @click="LogIn" class="w-full" />
            </form>
        </div>
    </div>
</template>

<script>
import { push } from 'notivue'
import api from '@/utilities/api.js'

export default {
    name: 'LoginView',
    data() {
        return {
            credentials: {
                UserName: "",
                Password: "",
            },
            invalid: {
                UserName: false,
                Password: false,
            }
        }
    },
    mounted() {
        localStorage.removeItem('sessionExpireTime');
        localStorage.removeItem('user');
        localStorage.removeItem('token');
    },
    methods: {
        async LogIn(event) {
            event.preventDefault();
            this.invalid.UserName = this.invalid.Password = false;

            if (this.credentials.UserName === "" || this.credentials.Password === "") {
                (this.credentials.UserName === "" ? this.invalid.UserName = true : this.invalid.UserName = false);
                (this.credentials.Password === "" ? this.invalid.Password = true : this.invalid.Password = false);
                push.warning("Por favor, rellene todos los campos")
            }
            else {
                localStorage.setItem("token", "token");
                localStorage.setItem("user", this.credentials.UserName);

                push.success("Ha iniciado sesión exitosamente");
                
                this.$store.commit('setUser', { nombreUsuario: this.credentials.UserName, nombrePerfil: "Administrador", nombres: "Ronnie Ismael", apellidos: "Difo de Leon"});
                
                const sessionExpireTime = new Date().getTime() + (30 * 60 * 1000);
                localStorage.setItem('sessionExpireTime', sessionExpireTime);
                
                this.$router.push('/');
                // const response = await api.post('/api/Account', this.credentials);
                // if (response.data.success) {
                //     const token = response.data.token;
                //     localStorage.setItem("token", token);
                //     localStorage.setItem("user", response.data.data.usuario.nombreUsuario);

                //     push.success(response.data.message);

                //     const { usuario, vistas } = response.data.data;
                    
                //     this.$store.commit('setUser', usuario);

                //     const sessionExpireTime = new Date().getTime() + (30 * 60 * 1000);
                //     localStorage.setItem('sessionExpireTime', sessionExpireTime);
                    
                //     this.$router.push('/');
                // }
                // else push.warning(response.data.message);
            }
        },
    }
}
</script>

<style>
.p-password-input, .p-password {
    width: 100% !important;
}
</style>