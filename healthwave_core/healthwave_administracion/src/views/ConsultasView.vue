<template>
  <div class="border-round-lg border-1 p-2 card h-full">
    <DataTable v-model:filters="filters" :value="consultas" paginator :rows="5" :rowsPerPageOptions="[5, 10, 20, 50]"
      paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
      currentPageReportTemplate="{first} a {last} de {totalRecords}" class="h-full">
      <template #header>
        <div class="flex justify-content-between align-items-center flex-wrap">
          <h1 class="text-lg">Listado de Consultas</h1>
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
      <template #empty> No se han encontrado consultas. </template>
      <Column field="consultaCodigo" header="#" style="width: 5%" sortable></Column>
      <Column field="documentoPaciente" header="Documento del Paciente" sortable></Column>
      <Column field="documentoMedico" header="Documento del Médico" sortable></Column>
      <Column field="idConsultorio" header="# de Consultorio" sortable></Column>
      <Column field="idAutorizacion" header="# de Autorización" sortable></Column>
      <Column field="fecha" header="Fecha" sortable></Column>
      <Column field="costo" header="Costo" sortable></Column>
      <Column field="estado" header="Estado" sortable></Column>
      <Column header="" style="width: 5%">
        <template #body="slotProps">
          <div class="flex justify-content-center gap-2">
            <Button icon="pi pi-pencil" severity="secondary" @click="Editar(slotProps.data)" type="button" text />
            <Button icon="pi pi-trash" severity="danger" @click="ConfirmarEliminar($event, slotProps.data.consultaCodigo)" type="button" text />
          </div>
        </template>
      </Column>
    </DataTable>
    <Dialog v-model:visible="mostrarFormulario" modal :header="(modoEdicion) ? 'Editar consulta' : 'Nueva consulta'"
      :style="{ width: '50vw' }" :breakpoints="{ '1199px': '75vw', '575px': '90vw' }">
      <div class="grid mb-2">
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Documento del paciente</label>
          <InputText v-model="model.documentoPaciente" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Documento del médico</label>
          <InputText v-model="model.documentoMedico" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Consultorio</label>
          <InputText v-model="model.idConsultorio" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Fecha</label>
          <Calendar v-model="model.fecha" hourFormat="12" placeholder="dd/mm/yy" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Costo</label>
          <InputNumber v-model="model.costo" inputId="integeronly" fluid class="w-full" />
        </div>
        <div class="col-12  md:col-6">
          <label class="font-semibold mb-2 w-full">Estado</label>
          <InputText v-model="model.estado" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Motivo</label>
          <Textarea v-model="model.motivo" autoResize rows="4" cols="30" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Comentarios</label>
          <Textarea v-model="model.comentarios" autoResize rows="4" cols="30" class="w-full" />
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
  name: "ConsultasView",
  created() {
    this.getConsultas();
  },
  data() {
    return {
      filters: {
        global: { value: null, matchMode: FilterMatchMode.CONTAINS },
      },
      consultas: [],
      model: {
        consultaCodigo: 0,
        documentoPaciente: "",
        documentoMedico: "",
        idConsultorio: 0,
        idAutorizacion: 0,
        fecha: "",
        motivo: "",
        comentarios: "",
        estado: "",
        costo: 0
      },
      mostrarFormulario: false,
      modoEdicion: false,
    };
  },
  props: {},
  methods: {
    async getConsultas() {
      this.consultas = [
        {
          consultaCodigo: 1,
          documentoPaciente: "123456789",
          documentoMedico: "987654321",
          idConsultorio: 1,
          idAutorizacion: 1,
          fecha: "2022-01-01",
          motivo: "Consulta de rutina",
          comentarios: "Sin comentarios",
          estado: "Pendiente",
          costo: 50
        },
        {
          consultaCodigo: 1,
          documentoPaciente: "123456789",
          documentoMedico: "987654321",
          idConsultorio: 1,
          idAutorizacion: 1,
          fecha: "2022-01-01",
          motivo: "Consulta de rutina",
          comentarios: "Sin comentarios",
          estado: "Pendiente",
          costo: 50
        },
      ];

      const response = await api.get('api/Consulta/Get');
      if (response.data && response.data.data != undefined) {
        this.consultas = response.data.data;
      }
    },
    async Guardar() {
      const response = await api[this.modoEdicion ? 'put' : 'post'](`api/Consulta/${this.modoEdicion ? `update` : 'add'}`, this.model);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha guardado la consulta exitosamente");
      } else {
        console.error('Error al agregar la consulta:', response.status, response.statusText);
        push.warning("Los datos ingresados no son válidos");
      }
    },
    async Eliminar(consultaCodigo) {
      const response = await api.delete(`api/Consulta/delete/${consultaCodigo}`);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha eliminado la consulta exitosamente");
      } else {
        console.error('Error al eliminar la consulta:', response.status, response.statusText);
        push.warning("Los datos ingresados no son válidos");
      }
    },
    ConfirmarEliminar(event, consultaCodigo) {
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
          await this.Eliminar(consultaCodigo);
        },
      })
    },
    Nuevo() {
      this.model = {
        consultaCodigo: 0,
        documentoPaciente: "",
        documentoMedico: "",
        idConsultorio: 0,
        idAutorizacion: 0,
        fecha: "",
        motivo: "",
        comentarios: "",
        estado: "",
        costo: 0
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