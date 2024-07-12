<template>
  <div class="border-round-lg border-1 p-2 card h-full">
    <DataTable
      v-model:filters="filters"
      :value="usuarios"
      paginator
      :rows="5"
      :rowsPerPageOptions="[5, 10, 20, 50]"
      paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
      currentPageReportTemplate="{first} a {last} de {totalRecords}"
      class="h-full"
    >
      <template #header>
        <div class="flex justify-content-between align-items-center">
          <h1 class="text-lg">Listado de Usuarios</h1>
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
      <template #empty> No se han encontrado usuarios. </template>
      <Column
        field="codigoDocumento"
        header="Código documento"
        sortable
      ></Column>
      <Column
        field="numLicenciaMedica"
        header="No. Licencia Médica"
        sortable
      ></Column>
      <Column field="nombre" header="Nombres" sortable></Column>
      <Column field="apellido" header="Apellidos" sortable></Column>
      <Column field="genero" header="Género" sortable></Column>
      <Column field="fechaNacimiento" header="Fecha de Nacimiento" sortable>
        <template #body="slotProps">
          {{ FormatearFecha(slotProps.data.fechaNacimiento) }}
        </template>
      </Column>
      <Column field="telefono" header="Teléfono" sortable></Column>
      <Column field="rol" header="Rol" sortable></Column>
      <Column header="">
        <template #body="slotProps">
          <div class="flex justify-content-between gap-2">
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
              @click="ConfirmarEliminar($event, slotProps.data.usuarioCodigo)"
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
      :header="modoEdicion ? 'Editar usuario' : 'Nuevo usuario'"
      :style="{ width: '50vw' }"
      :breakpoints="{ '1199px': '75vw', '575px': '90vw' }"
    >
      <div class="grid mb-2">
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Nombre de Usuario</label>
          <InputText v-model="model.usuarioCodigo" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Rol</label>
          <Dropdown
            v-model="model.rol"
            class="w-full"
            :options="roles"
            optionLabel="label"
            optionValue="value"
            placeholder="Seleccione..."
            emptyMessage="No se han encontrado roles"
          />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Tipos de Documento</label>
          <Dropdown
            v-model="model.tipoDocumento"
            class="w-full"
            :options="tiposDocumento"
            optionLabel="label"
            optionValue="value"
            placeholder="Seleccione..."
            emptyMessage="No se han encontrado tipos de documento"
          />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Código de Documento</label>
          <InputText v-model="model.codigoDocumento" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Nombres</label>
          <InputText v-model="model.nombre" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Apellidos</label>
          <InputText v-model="model.apellido" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Fecha de Nacimiento</label>
          <DatePicker
            v-model="model.fechaNacimiento"
            dateFormat="yy-mm-dd"
            :showIcon="true"
            inputId="fecha"
            class="w-full"
          />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Género</label>
          <Dropdown
            v-model="model.genero"
            class="w-full"
            :options="generos"
            optionLabel="label"
            optionValue="value"
            placeholder="Seleccione..."
            emptyMessage="No se han encontrado géneros"
          />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full"
            >Número de Licencia Médica</label
          >
          <InputText v-model="model.numLicenciaMedica" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Teléfono</label>
          <InputText v-model="model.telefono" class="w-full" />
        </div>
        <div class="col-12">
          <label class="font-semibold mb-2 w-full">Correo</label>
          <InputText v-model="model.correo" class="w-full" />
        </div>
        <div class="col-12">
          <label class="font-semibold mb-2 w-full">Dirección</label>
          <Textarea
            v-model="model.direccion"
            autoResize
            rows="4"
            cols="30"
            class="w-full"
          />
        </div>
      </div>
      <div class="flex justify-content-end gap-2">
        <Button
          type="button"
          label="Cancelar"
          severity="secondary"
          @click="visible = false"
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
  name: "UsuariosView",
  created() {
    this.getUsuarios();
  },
  data() {
    return {
      filters: {
        global: { value: null, matchMode: FilterMatchMode.CONTAINS },
      },
      usuarios: [],
      model: {
        usuarioCodigo: "",
        codigoDocumento: "",
        usuarioContra: "",
        tipoDocumento: "",
        numLicenciaMedica: "",
        nombre: "",
        apellido: "",
        genero: "",
        fechaNacimiento: "",
        telefono: "",
        correo: "",
        direccion: "",
        rol: "",
      },
      mostrarFormulario: false,
      modoEdicion: false,
      tiposDocumento: [
        { label: "Identificación", value: "I" },
        { label: "Pasaporte", value: "P" },
      ],
      generos: [
        { label: "Masculino", value: "M" },
        { label: "Femenino", value: "F" },
      ],
      roles: [
        { label: "Administrador", value: "A" },
        { label: "Paciente", value: "P" },
        { label: "Medico", value: "M" },
        { label: "Enfermero", value: "E" },
        { label: "Caja", value: "C" },
      ],
    };
  },
  props: {},
  methods: {
    async getUsuarios() {
      this.usuarios = [
        {
          usuarioCodigo: "string",
          codigoDocumento: "string",
          usuarioContra: "string",
          tipoDocumento: "I",
          numLicenciaMedica: "string",
          nombre: "string",
          apellido: "string",
          genero: "M",
          fechaNacimiento: "2024-07-03T04:17:48.081Z",
          telefono: "string",
          correo: "string",
          direccion: "string",
          rol: "A",
        },
      ];

      const response = await api.get("api/Usuario/get");
      console.log(response);
      if (response.data) {
        this.usuarios = response.data;
        this.mostrarFormulario = false; // Close the dialog
        await this.getConsultas(); // Refresh the data table
      }
    },
    async Guardar() {
      const response = await api[this.modoEdicion ? "put" : "post"](
        `api/Usuario/${this.modoEdicion ? "Update" : "Add"}`,
        this.model
      );
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha guardado el usuario exitosamente");
      } else {
        console.error(
          "Error al agregar el usuario:",
          response.status,
          response.statusText
        );
        push.warning("Los datos ingresados no son válidos");
      }
    },
    async Eliminar(codigo) {
      const response = await api.delete(`api/Usuario/Delete/${codigo}`);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha eliminado el usuario exitosamente");
      } else {
        console.error(
          "Error al eliminar el usuario:",
          response.status,
          response.statusText
        );
        push.warning("Los datos ingresados no son válidos");
      }
    },
    ConfirmarEliminar(event, codigo) {
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
          await this.Eliminar(codigo);
        },
      });
    },
    Nuevo() {
      this.model = {
        usuarioCodigo: "",
        codigoDocumento: "",
        usuarioContra: "",
        tipoDocumento: "",
        numLicenciaMedica: "",
        nombre: "",
        apellido: "",
        genero: "",
        fechaNacimiento: "",
        telefono: "",
        correo: "",
        direccion: "",
        rol: "",
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
