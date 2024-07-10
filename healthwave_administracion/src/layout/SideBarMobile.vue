<template>
  <div>
    <Button severity="secondary" size="large" text icon="pi pi-bars" class="p-1 text-color-secondary" @click="sidebarVisible = true" />
    <Drawer v-model:visible="sidebarVisible" class="m-2 border-round-2xl" blockScroll :pt="
      {
        root: { style: 'height: 98%' },
        close: { class: 'bg-white border-round-2xl' },
        mask: { class: 'bg-black' },
      }
    ">
      <template #container>
        <Menu :model="items" class="flex w-full md:w-60 h-full bg-transparent border-none flex-column " :pt="{
          list: { class: 'flex flex-1 px-3 overflow-y-auto	' },
        }">
          <template #start>
            <span class="flex justify-content-center align-items-center gap-3 p-3 w-full">
              <img src="@/assets/logo.png" alt="CGRD" style="height: 5rem; object-fit: contain" />
            </span>
            <div class="p-menu-separator mb-1" />
          </template>
          <template #submenulabel="{ item }">
            <span class="text-primary text-sm font-semibold">{{ item.label }}</span>
          </template>
          <template #item="{ item }">
            <SideBarItem :item="item" />
          </template>
        </Menu>
      </template>
  </Drawer>
  </div>
</template>

<script>
export default {
  name: "SideBarMobile",
  created() {},
  data() {
    return {
      sidebarVisible: false,
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
    };
  },
  props: {},
  methods: {},
};
</script>

<style lang="scss" scoped></style>