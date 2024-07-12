<template>
  <div class="border-round-lg border-1 p-2 card h-full">
    <DataTable
      v-model:filters="filters"
      :value="Servicios"
      paginator
      :rows="5"
      :rowsPerPageOptions="[5, 10, 20, 50]"
      paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
      currentPageReportTemplate="{first} a {last} de {totalRecords}"
      class="h-full"
    >
      <template #header>
        <div class="flex justify-content-between align-items-center flex-wrap">
          <h1 class="text-lg">Listado de Servicios</h1>
          <div class="flex justify-content-between align-items-center gap-4">
            <Button
              type="button"
              label="Agregar"
              icon="pi pi-plus"
              size="normal"
              @click="Nuevo()"
            />
            <IconField>
              <InputIcon>
                <i class="pi pi-search" />
              </InputIcon>
              <InputText
                v-model="filters['global'].value"
                placeholder="Buscar..."
                size="normal"
              />
            </IconField>
          </div>
        </div>
      </template>
      <template #empty> No se han encontrado Servicios. </template>
      <Column
        field="ServicioCodigo"
        header="#"
        sortable
        style="width: 5%"
      ></Column>
      <Column 
      field="Nombre" header="Nombre" sortable
      ></Column>
      <Column field="Descripcion" header="Descripción" sortable></Column>
      <Column
        field="IDTipoServicio"
        header="Tipo de servicio"
        sortable
      ></Column>
      <Column field="Costo" header="Costo" sortable></Column>
      <Column
        field="idAseguradora"
        header="# Id de Aseguradora"
        sortable
      ></Column>
      <Column header="" style="width: 5%">
        <template #body="slotProps">
          <div class="flex justify-content-center gap-2">
            <Button
              icon="pi pi-pencil"
              severity="secondary"
              @click="Editar(slotProps.data)"
              type="button"
              text
            />
            <Button
              icon="pi pi-trash"
              severity="danger"
              @click="ConfirmarEliminar($event, slotProps.data.idConsultorio)"
              type="button"
              text
            />
          </div>
        </template>
      </Column>
    </DataTable>
    <Dialog
      v-model:visible="mostrarFormulario"
      modal
      :header="modoEdicion ? 'Editar Servicio' : 'Nuevo Servicio'"
      :style="{ width: '50vw' }"
      :breakpoints="{ '1199px': '75vw', '575px': '90vw' }"
    >
    <div class="grid mb-2">
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full"
          >Codigo del Servicio</label
          >
          <InputText v-model="model.servicioCodigo" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full"
          >Nombre del Servicio</label
          >
          <InputText v-model="model.nombre" class="w-f  ull" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Descripcion</label>
          <InputMask id="telefono" v-model="model.descripcion" class="w-full" />
        </div>
        <div class="col-12">
          <label class="font-semibold mb-2 w-full">Tipo de servicio</label>
          <Textarea v-model="model.idTipoServicio" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Costo</label>
          <InputNumber
            v-model="model.costo"
            inputId="integeronly"
            fluid
            class="w-full"
          />
        </div>
        <div>
        <label class="font-semibold mb-2 w-full">#Id Aseguradora</label>
          <InputNumber
            v-model="model.idAseguradora"
            inputId="integeronly"
            fluid
            class="w-full"
          />
        </div>
      </div>
      <div class="flex justify-content-end gap-2">
        <Button
          type="button"
          label="Cancelar"
          severity="secondary"
          @click="mostrarFormulario = false"
        ></Button>
        <Button type="button" label="Guardar" @click="Guardar()"></Button>
      </div>
    </Dialog>
    <ConfirmPopup></ConfirmPopup>
  </div>
</template>

<script>
import { FilterMatchMode } from "@primevue/core/api";
import api from "@/utilities/api.js";
import { push } from "notivue";

export default {
  name: "ServiciosView",
  created() {
    this.getServicios();
  },
  data() {
    return {
      filters: {
        global: { value: null, matchMode: FilterMatchMode.CONTAINS },
      },
      servicios: [],
      model: {
        servicioCodigo: 0,
        nombre: "",
        descripcion: "",
        idTiposervicio: "",
        costo: 0,
        idAseguradora: 0,
      },
      mostrarFormulario: false,
      modoEdicion: false,
    };
  },
  props: {},
  methods: {
    async getServicios() {
      this.servicios = [
        {
          servicioCodigo: 2,
          nombre: "Cirugía de Emergencia",
          descripcion: "Intervención quirúrgica urgente",
          idTipoServicio: 9,
          costo: 500,
          idAseguradora: 10,
        },
        {
          servicioCodigo: 3,
          nombre: "Análisis de Sangre",
          descripcion: "Pruebas de laboratorio para análisis de sangre",
          idTipoServicio: null,
          costo: 502,
          idAseguradora: null,
        },
      ];

      const response = await api.get("api/Servicios/get");
      if (response.data) {
        this.servicios = response.data;
      }
    },
    async Guardar() {
      const response = await api[this.modoEdicion ? "put" : "post"](
        `api/Servicios/${this.modoEdicion ? "update" : "post"}`,
        this.model
      );
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha guardado el consultorio exitosamente");
      } else {
        console.error(
          "Error al agregar el consultorio:",
          response.status,
          response.statusText
        );
        push.warning("Los datos ingresados no son válidos");
      }
    },
    async Eliminar(servicioCodigo) {
      const response = await api.delete(
        `api/Servicios/Delete/${servicioCodigo}`
      );
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha eliminado el consultorio exitosamente");
      } else {
        console.error(
          "Error al eliminar el consultorio:",
          response.status,
          response.statusText
        );
        push.warning("Los datos ingresados no son válidos");
      }
    },
    ConfirmarEliminar(event, servicioCodigo) {
      this.$confirm.require({
        target: event.currentTarget,
        message: "¿Estás seguro que deseas eliminar este registro?",
        icon: "pi pi-info-circle",
        rejectProps: {
          label: "Cancelar",
          severity: "secondary",
          outlined: true,
        },
        acceptProps: {
          label: "Eliminar",
          severity: "danger",
        },
        accept: async () => {
          await this.Eliminar(servicioCodigo);
        },
      });
    },
    Nuevo() {
      this.model = {
        servicioCodigo: 0,
        nombre: "",
        descripcion: "",
        idTipoServicio: "",
        costo: 0,
        idAseguradora: 0,
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
    },
  },
};
</script>
