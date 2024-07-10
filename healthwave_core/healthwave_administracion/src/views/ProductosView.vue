<template>
  <div class="border-round-lg border-1 p-2 card h-full">
    <DataTable v-model:filters="filters" :value="productos" paginator :rows="5" :rowsPerPageOptions="[5, 10, 20, 50]"
      paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
      currentPageReportTemplate="{first} a {last} de {totalRecords}" class="h-full">
      <template #header>
        <div class="flex justify-content-between align-items-center flex-wrap">
          <h1 class="text-lg">Listado de Productos</h1>
          <div class="flex justify-content-between align-items-center gap-4">
            <Button type="button" label="Agregar" icon="pi pi-plus" size="normal" @click="Nuevo()"/>
            <IconField>
              <InputIcon>
                <i class="pi pi-search" />
              </InputIcon>
              <InputText v-model="filters['global'].value" placeholder="Buscar..." size="normal" />
            </IconField>
          </div>
        </div>
      </template>
      <template #empty> No se han encontrado productos. </template>
      <Column field="idProducto" header="#" style="width: 5%" sortable></Column>
      <Column field="nombre" header="Nombre" sortable></Column>
      <Column field="descripcion" header="Descripción" sortable></Column>
      <Column field="costo" header="Precio" sortable></Column>
      <Column header="" style="width: 5%">
        <template #body="slotProps">
          <div class="flex justify-content-center gap-2">
            <Button icon="pi pi-pencil" severity="secondary" @click="Editar(slotProps.data)" type="button" text />
            <Button icon="pi pi-trash" severity="danger" @click="ConfirmarEliminar($event, slotProps.data.idProducto)" type="button" text />
          </div>
        </template>
      </Column>
    </DataTable>
    <Dialog v-model:visible="mostrarFormulario" modal :header="(modoEdicion) ? 'Editar producto' : 'Nuevo producto'"
      :style="{ width: '50vw' }" :breakpoints="{ '1199px': '75vw', '575px': '90vw' }">
      <div class="grid mb-2">
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Nombre</label>
          <InputText v-model="model.nombre" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Precio</label>
          <InputNumber v-model="model.costo" inputId="integeronly" fluid class="w-full" />
        </div>
        <div class="col-12">
          <label class="font-semibold mb-2 w-full">Descripción</label>
          <Textarea v-model="model.descripcion" autoResize rows="4" cols="30" class="w-full" />
        </div>
      </div>
      <div class="flex justify-content-end gap-2">
        <Button type="button" label="Cancelar" severity="secondary" @click="mostrarFormulario = false"></Button>
        <Button type="button" label="Guardar" @click="Guardar()"></Button>
      </div>
    </Dialog>
    <ConfirmPopup></ConfirmPopup>

  </div>
</template>

<script>
import { FilterMatchMode } from '@primevue/core/api';
import api from '@/utilities/api.js';
import { push } from 'notivue'

export default {
  name: "ProductosView",
  created() {
    this.getProductos();
  },
  data() {
    return {
      filters: {
        global: { value: null, matchMode: FilterMatchMode.CONTAINS },
      },
      productos: [],
      model: {
        idProducto: 0,
        nombre: "",
        descripcion: "",
        costo: 0,
      },
      mostrarFormulario: false,
      modoEdicion: false,
    };
  },
  props: {},
  methods: {
    async getProductos() {
      this.productos = [
      {
        idProducto: 1,
        nombre: "Laptop",
        descripcion: "Laptop con 16GB de RAM y 512GB de SSD",
        costo: 1200
      },
      {
        idProducto: 2,
        nombre: "Smartphone",
        descripcion: "Smartphone con pantalla de 6.5 pulgadas y 128GB de almacenamiento",
        costo: 700
      },
      {
        idProducto: 3,
        nombre: "Auriculares",
        descripcion: "Auriculares inalámbricos con cancelación de ruido",
        costo: 150
      },
      {
        idProducto: 4,
        nombre: "Monitor",
        descripcion: "Monitor 4K de 27 pulgadas",
        costo: 400
      },
      {
        idProducto: 5,
        nombre: "Teclado Mecánico",
        descripcion: "Teclado mecánico con retroiluminación RGB",
        costo: 100
      }
    ];

      const response = await api.get('api/Producto/get');
      if (response.data) {
        this.productos = response.data.data;
      }
    },
    async Guardar() {
      const response = await api[this.modoEdicion ? 'put' : 'post'](`api/Producto/${this.modoEdicion ? 'update' : 'post'}`, this.model);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha guardado la producto exitosamente");
      } else {
        console.error('Error al agregar la producto:', response.status, response.statusText);
        push.warning("Los datos ingresados no son válidos");
      }
    },
    async Eliminar(idProducto) {
      const response = await api.delete(`api/Producto/Delete/${idProducto}`);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha eliminado la producto exitosamente");
      } else {
        console.error('Error al eliminar la producto:', response.status, response.statusText);
        push.warning("Los datos ingresados no son válidos");
      }
    },
    ConfirmarEliminar(event, idProducto) {
      this.$confirm.require({
        target: event.currentTarget,
        message: '¿Estás seguro que deseas eliminar este registro?',
        icon: 'pi pi-info-circle',
        rejectProps: {
          label: 'Cancelar',
          severity: 'secondary',
          outlined: true
        },
        acceptProps: {
          label: 'Eliminar',
          severity: 'danger'
        },
        accept: async () => {
          await this.Eliminar(idProducto);
        },
      })
    },
    Nuevo() {
      this.model = {
        idProducto: 0,
        nombre: "",
        descripcion: "",
        costo: 0,
      },
      this.modoEdicion = false;
      this.mostrarFormulario = true;
    },
    Editar(model) {
      this.model = Object.assign(this.model, model);
      this.modoEdicion = true;
      this.mostrarFormulario = true;
    },
    FormatearFecha(date) {
      return new Date(date).toLocaleDateString();
    }
  },
};
</script>