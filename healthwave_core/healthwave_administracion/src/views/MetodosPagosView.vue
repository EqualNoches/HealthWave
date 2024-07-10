<template>
  <div class="border-round-lg border-1 p-2 card h-full">
    <DataTable v-model:filters="filters" :value="metodosPago" paginator :rows="5" :rowsPerPageOptions="[5, 10, 20, 50]"
      paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
      currentPageReportTemplate="{first} a {last} de {totalRecords}" class="h-full">
      <template #header>
        <div class="flex justify-content-between align-items-center flex-wrap">
          <h1 class="text-lg">Listado de Métodos de pago</h1>
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
      <template #empty> No se han encontrado métodos de pago. </template>
      <Column field="codigoMetodoDePago" header="#" style="width: 5%" sortable></Column>
      <Column field="nombre" header="Nombre" sortable></Column>
      <Column header="" style="width: 5%">
        <template #body="slotProps">
          <div class="flex justify-content-center gap-2">
            <Button icon="pi pi-pencil" severity="secondary" @click="Editar(slotProps.data)" type="button" text />
            <Button icon="pi pi-trash" severity="danger" @click="ConfirmarEliminar($event, slotProps.data.codigoMetodoDePago)" type="button" text />
          </div>
        </template>
      </Column>
    </DataTable>
    <Dialog v-model:visible="mostrarFormulario" modal :header="(modoEdicion) ? 'Editar método de pago' : 'Nuevo método de pago'"
      :style="{ width: '50vw' }" :breakpoints="{ '1199px': '75vw', '575px': '90vw' }">
      <div class="grid mb-2">
        <div class="col-12">
          <label class="font-semibold mb-2 w-full">Nombre</label>
          <InputText v-model="model.nombre" class="w-full" />
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
  name: "MetodosPagosView",
  created() {
    this.getMetodosPago();
  },
  data() {
    return {
      filters: {
        global: { value: null, matchMode: FilterMatchMode.CONTAINS },
      },
      metodosPago: [],
      model: {
        codigoMetodoDePago: 0,
        nombre: "",
      },
      mostrarFormulario: false,
      modoEdicion: false,
    };
  },
  props: {},
  methods: {
    async getMetodosPago() {
      this.metodosPago = [
        {
          codigoMetodoDePago: 1,
          nombre: "Tarjeta de Crédito"
        },
        {
          codigoMetodoDePago: 2,
          nombre: "PayPal"
        },
        {
          codigoMetodoDePago: 3,
          nombre: "Transferencia Bancaria"
        },
        {
          codigoMetodoDePago: 4,
          nombre: "Efectivo"
        },
        {
          codigoMetodoDePago: 5,
          nombre: "Apple Pay"
        }
      ];

      const response = await api.get('api/MetodoDePago/GET');
      if (response.data) {
        this.metodosPago = response.data.data;
      }
    },
    async Guardar() {
      const response = await api[this.modoEdicion ? 'put' : 'post'](`api/MetodoDePago/${this.modoEdicion ? 'update' : 'ADD'}`, this.model);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha guardado el método de pago exitosamente");
      } else {
        console.error('Error al agregar el método de pago:', response.status, response.statusText);
        push.warning("Los datos ingresados no son válidos");
      }
    },
    async Eliminar(codigoMetodoDePago) {
      const response = await api.delete(`api/MetodoDePago/delete/${codigoMetodoDePago}`);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha eliminado el método de pago exitosamente");
      } else {
        console.error('Error al eliminar el método de pago:', response.status, response.statusText);
        push.warning("Los datos ingresados no son válidos");
      }
    },
    ConfirmarEliminar(event, codigoMetodoDePago) {
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
          await this.Eliminar(codigoMetodoDePago);
        },
      })
    },
    Nuevo() {
      this.model = {
        codigoMetodoDePago: 0,
        nombre: "",
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