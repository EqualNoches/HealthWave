<template>
    <section class="m-auto">
      <div class="flex flex-column gap-2 text-center max-w-25rem">
        <div>
          <h1 style="font-size: 100px;" class="my-0">404</h1>
          <h2 class="mt-0">Página no encontrada</h2>
          <p>Es posible que la página que está buscando se haya eliminado, se haya cambiado de nombre o no esté disponible temporalmente.</p>
        </div>
        <router-link to="/">
          <Button label="Volver al inicio" />
        </router-link>
      </div>
    </section>
  </template>
  
  <script>
  export default {
    data() {
      return {};
    },
    mounted() {},
  
    methods: {},
  };
  </script>