<template>
    <div class="container mx-auto p-4">
      <div class="text-center mb-6">
        <h1 class="text-4xl font-bold text-gray-800">Bienvenido a HealthWave</h1>
        <p class="text-gray-600 mt-2">Gestiona todos los servicios y recursos de manera eficiente</p>
      </div>
  
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <div class="bg-white rounded-lg shadow-md p-6">
          <div class="flex items-center justify-center mb-4">
            <i class="pi pi-users text-blue-500 text-4xl"></i>
          </div>
          <h2 class="text-xl font-semibold text-gray-800 text-center">Gestión de Usuarios</h2>
          <p class="text-gray-600 mt-2 text-center">Administra la información de los pacientes y sus historiales médicos.</p>
        </div>
  
        <div class="bg-white rounded-lg shadow-md p-6">
          <div class="flex items-center justify-center mb-4">
            <i class="pi pi-calendar text-green-500 text-4xl"></i>
          </div>
          <h2 class="text-xl font-semibold text-gray-800 text-center">Consultas</h2>
          <p class="text-gray-600 mt-2 text-center">Programa y gestiona las citas médicas de manera fácil y rápida.</p>
        </div>
  
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: "Inicio"
  };
  </script>
  
  <style scoped>
  .container {
    max-width: 1200px;
  }
  </style>
  