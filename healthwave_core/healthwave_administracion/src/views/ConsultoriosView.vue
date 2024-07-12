<template>
  <div class="border-round-lg border-1 p-2 card h-full">
    <DataTable v-model:filters="filters" :value="consultorios" paginator :rows="5" :rowsPerPageOptions="[5, 10, 20, 50]"
      paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
      currentPageReportTemplate="{first} a {last} de {totalRecords}" class="h-full">
      <template #header>
        <div class="flex justify-content-between align-items-center flex-wrap">
          <h1 class="text-lg">Listado de Consultorios</h1>
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
      <template #empty> No se han encontrado consultorios. </template>
      <Column field="idConsultorio" header="#" sortable style="width: 5%"></Column>
      <Column field="nombre" header="Nombre" sortable></Column>
      <Column field="telefono" header="Teléfono" sortable></Column>
      <Column field="direccion" header="Dirección" sortable></Column>
      <Column header="" style="width: 5%">
        <template #body="slotProps">
          <div class="flex justify-content-center gap-2">
            <Button icon="pi pi-pencil" severity="secondary" @click="Editar(slotProps.data)" type="button" text />
            <Button icon="pi pi-trash" severity="danger" @click="ConfirmarEliminar($event, slotProps.data.idConsultorio)" type="button" text />
          </div>
        </template>
      </Column>
    </DataTable>
    <Dialog v-model:visible="mostrarFormulario" modal :header="(modoEdicion) ? 'Editar consultorio' : 'Nuevo consultorio'"
      :style="{ width: '50vw' }" :breakpoints="{ '1199px': '75vw', '575px': '90vw' }">
      <div class="grid mb-2">
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Nombre del Consultorio</label>
          <InputText v-model="model.nombre" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Teléfono</label>
          <InputMask id="telefono" v-model="model.telefono" mask="(999) 999-9999" placeholder="(999) 999-9999" fluid class="w-full" />
        </div>
        <div class="col-12">
          <label class="font-semibold mb-2 w-full">Dirección</label>
          <Textarea v-model="model.direccion" autoResize rows="4" cols="30" class="w-full" />
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
  name: "ConsultoriosView",
  created() {
    this.getConsultorios();
  },
  data() {
    return {
      filters: {
        global: { value: null, matchMode: FilterMatchMode.CONTAINS },
      },
      consultorios: [],
      model: {
        idConsultorio: 0,
        nombre: "",
        direccion: "",
        telefono: "",
        consultaCodigo: []
      },
      mostrarFormulario: false,
      modoEdicion: false,
    };
  },
  props: {},
  methods: {
    async getConsultorios() {
      this.consultorios = [
        {
          idConsultorio: 0,
          nombre: "string",
          direccion: "string",
          telefono: "string",
          consultaCodigo: []
        }
      ];  

      const response = await api.get('api/Consultorio');
      if (response.data) {
        this.consultorios = response.data;
      }
    },
    async Guardar() {
      const response = await api[this.modoEdicion ? 'put' : 'post'](`api/Consultorio/${this.modoEdicion ? 'update' : 'add'}`, this.model);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha guardado el consultorio exitosamente");
      } else {
        console.error('Error al agregar el consultorio:', response.status, response.statusText);
        push.warning("Los datos ingresados no son válidos");
      }
    },
    async Eliminar(idConsultorio) {
      const response = await api.delete(`api/Consultorio/Delete/${idConsultorio}`);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha eliminado el consultorio exitosamente");
      } else {
        console.error('Error al eliminar el consultorio:', response.status, response.statusText);
        push.warning("Los datos ingresados no son válidos");
      }
    },
    ConfirmarEliminar(event, idConsultorio) {
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
          await this.Eliminar(idConsultorio);
        },
      })
    },
    Nuevo() {
      this.model = {
        idConsultorio: 0,
        nombre: "",
        direccion: "",
        telefono: "",
        consultaCodigo: []
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