import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

import { createNotivue } from 'notivue'

import PrimeVue from 'primevue/config';
import { definePreset, palette } from '@primevue/themes';
import Aura from '@primevue/themes/aura';
import Tooltip from 'primevue/tooltip';
import ConfirmationService from 'primevue/confirmationservice';
import esLocale from '@/utilities/es_locale';

import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import {dom, library} from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons'
import { fab } from '@fortawesome/free-brands-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';

import 'notivue/notifications.css' 
import 'notivue/animations.css'
import 'primeflex/primeflex.css'
import 'primeicons/primeicons.css'
import '@/styles/main.css'

const notivue = createNotivue({
    position: 'top-right',
    limit: 4, 
    enqueue: true,
    notifications: {
      global: {
        duration: 3000
      }
    }
  })

library.add(fas, fab, far);
dom.watch()

const app = createApp(App)

const paleta = palette('#1192A6');

const Noir = definePreset(Aura, {
  semantic: {
      primary: paleta,
      colorScheme: {
          light: {
              primary: {
                  color: paleta[600],
                  inverseColor: '#ffffff',
                  hoverColor: paleta[600],
                  activeColor: paleta[700]
              },
              highlight: {
                  background: paleta[100],
                  focusBackground: paleta[600],
              },
              surface: {
                0: '#ffffff',
                50: '{slate.50}',
                100: '{slate.100}',
                200: '{slate.200}',
                300: '{slate.300}',
                400: '{slate.400}',
                500: '{slate.500}',
                600: '{slate.600}',
                700: '{slate.700}',
                800: '{slate.800}',
                900: '{slate.900}',
                950: '{slate.950}'
            },
          },
          dark: {
              primary: {
                color: '{zinc.50}',
                inverseColor: '{zinc.950}',
                hoverColor: '{zinc.100}',
                activeColor: '{zinc.200}'
              },
              highlight: {
                background: 'rgba(250, 250, 250, .16)',
                focusBackground: 'rgba(250, 250, 250, .24)',
                color: 'rgba(255,255,255,.87)',
                focusColor: 'rgba(255,255,255,.87)'
              },
              surface: {
                0: '#ffffff',
                50: '{zinc.50}',
                100: '{zinc.100}',
                200: '{zinc.200}',
                300: '{zinc.300}',
                400: '{zinc.400}',
                500: '{zinc.500}',
                600: '{zinc.600}',
                700: '{zinc.700}',
                800: '{zinc.800}',
                900: '{zinc.900}',
                950: '{zinc.950}'
            }
          }
      }
  }
});

app.use(notivue);
app.use(router);
app.use(store);
app.use(PrimeVue, {
  locale: esLocale,
  theme: {
      preset: Noir,
      options: {
          prefix: 'p',
          darkModeSelector: '.p-dark',
      }
  }
});
app.use(ConfirmationService);
app.directive('tooltip', Tooltip);
app.component("font-awesome-icon", FontAwesomeIcon)
app.mount('#app')