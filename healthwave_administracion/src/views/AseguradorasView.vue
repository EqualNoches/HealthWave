<template>
  <div class="border-round-lg border-1 p-2 card h-full">
    <DataTable v-model:filters="filters" :value="aseguradoras" paginator :rows="5" :rowsPerPageOptions="[5, 10, 20, 50]"
      paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
      currentPageReportTemplate="{first} a {last} de {totalRecords}" class="h-full">
      <template #header>
        <div class="flex justify-content-between align-items-center flex-wrap">
          <h1 class="text-lg">Listado de Aseguradoras</h1>
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
      <template #empty> No se han encontrado aseguradoras. </template>
      <Column field="idAseguradora" header="#" style="width: 5%" sortable></Column>
      <Column field="nombre" header="Nombre" sortable></Column>
      <Column field="telefono" header="Teléfono" sortable></Column>
      <Column field="dirección" header="Dirección" sortable></Column>
      <Column header="" style="width: 5%">
        <template #body="slotProps">
          <div class="flex justify-content-center gap-2">
            <Button icon="pi pi-pencil" severity="secondary" @click="Editar(slotProps.data)" type="button" text />
            <Button icon="pi pi-trash" severity="danger" @click="ConfirmarEliminar($event, slotProps.data.idAseguradora)" type="button" text />
          </div>
        </template>
      </Column>
    </DataTable>
    <Dialog v-model:visible="mostrarFormulario" modal :header="(modoEdicion) ? 'Editar aseguradora' : 'Nuevo aseguradora'"
      :style="{ width: '50vw' }" :breakpoints="{ '1199px': '75vw', '575px': '90vw' }">
      <div class="grid mb-2">
        <div class="col-12">
          <label class="font-semibold mb-2 w-full">Nombre</label>
          <InputText v-model="model.nombre" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Teléfono</label>
          <InputMask id="telefono" v-model="model.telefono" mask="(999) 999-9999" placeholder="(999) 999-9999" fluid class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Correo</label>
          <InputText v-model="model.correo" class="w-full" />
        </div>
        <div class="col-12">
          <label class="font-semibold mb-2 w-full">Dirección</label>
          <Textarea v-model="model.dirección" autoResize rows="4" cols="30" class="w-full" />
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
  name: "AseguradorasView",
  created() {
    this.getAseguradoras();
  },
  data() {
    return {
      filters: {
        global: { value: null, matchMode: FilterMatchMode.CONTAINS },
      },
      aseguradoras: [],
      model: {
        idAseguradora: 0,
        nombre: "",
        dirección: "",
        telefono: "",
        correo: "",
        autorizacions: [],
        servicios: []
      },
      mostrarFormulario: false,
      modoEdicion: false,
    };
  },
  props: {},
  methods: {
    async getAseguradoras() {
      this.aseguradoras = [
        {
          idAseguradora: 1,
          nombre: "asdas sadsa",
          dirección: "asd as",
          telefono: "sdasd",
          correo: "asdasdsada",
          autorizacions: [],
          servicios: []
        }
      ];

      const response = await api.get('api/Aseguradora/get');
      if (response.data) {
        this.aseguradoras = response.data.data;
      }
    },
    async Guardar() {
      const response = await api[this.modoEdicion ? 'put' : 'post'](`api/Aseguradora/${this.modoEdicion ? 'update' : 'post'}`, this.model);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha guardado la aseguradora exitosamente");
      } else {
        console.error('Error al agregar la aseguradora:', response.status, response.statusText);
        push.warning("Los datos ingresados no son válidos");
      }
    },
    async Eliminar(idAseguradora) {
      const response = await api.delete(`api/Aseguradora/Delete/${idAseguradora}`);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha eliminado la aseguradora exitosamente");
      } else {
        console.error('Error al eliminar la aseguradora:', response.status, response.statusText);
        push.warning("Los datos ingresados no son válidos");
      }
    },
    ConfirmarEliminar(event, idAseguradora) {
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
          await this.Eliminar(idAseguradora);
        },
      })
    },
    Nuevo() {
      this.model = {
        idAseguradora: 0,
        nombre: "",
        dirección: "",
        telefono: "",
        correo: "",
        autorizacions: [],
        servicios: []
      };
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