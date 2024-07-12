<template>
  <div class="border-round-lg border-1 p-2 card h-full">
    <DataTable v-model:filters="filters" :value="cuentasPorCobrar" paginator :rows="5" :rowsPerPageOptions="[5, 10, 20, 50]"
      paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
      currentPageReportTemplate="{first} a {last} de {totalRecords}" class="h-full">
      <template #header>
        <div class="flex justify-content-between align-items-center flex-wrap">
          <h1 class="text-lg">Listado de Cuentas por Cobrar</h1>
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
      <template #empty> No se han encontrado cuentas por cobrar. </template>
      <Column field="idcuenta" header="#" style="width: 5%" sortable></Column>
      <Column field="codigoPaciente" header="Código del Paciente" sortable></Column>
      <Column field="balance" header="Balance" sortable></Column>
      <Column field="estado" header="Estado" sortable></Column>
      <Column header="" style="width: 5%">
        <template #body="slotProps">
          <div class="flex justify-content-center gap-2">
            <Button icon="pi pi-pencil" severity="secondary" @click="Editar(slotProps.data)" type="button" text />
            <Button icon="pi pi-trash" severity="danger" @click="ConfirmarEliminar($event, slotProps.data.idcuenta)" type="button" text />
          </div>
        </template>
      </Column>
    </DataTable>
    <Dialog v-model:visible="mostrarFormulario" modal :header="(modoEdicion) ? 'Editar cuenta por cobrar' : 'Nueva cuenta por cobrar'"
      :style="{ width: '50vw' }" :breakpoints="{ '1199px': '75vw', '575px': '90vw' }">
      <div class="grid mb-2">
        <div class="col-12">
          <label class="font-semibold mb-2 w-full">Código del paciente</label>
          <InputText v-model="model.codigoPaciente" class="w-full" />
        </div>
        <div class="col-12 md:col-6">
          <label class="font-semibold mb-2 w-full">Balance</label>
          <InputNumber v-model="model.balance" inputId="integeronly" fluid class="w-full" />
        </div>
        <div class="col-12  md:col-6">
          <label class="font-semibold mb-2 w-full">Estado</label>
          <InputText v-model="model.estado" class="w-full" />
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
  name: "CuentasPorCobrarView",
  created() {
    this.getCuentasPorCobrar();
  },
  data() {
    return {
      filters: {
        global: { value: null, matchMode: FilterMatchMode.CONTAINS },
      },
      cuentasPorCobrar: [],
      model: {
        idcuenta: 0,
        balance: 0,
        estado: "",
        codigoPaciente: ""
      },
      mostrarFormulario: false,
      modoEdicion: false,
    };
  },
  props: {},
  methods: {
    async getCuentasPorCobrar() {
      this.cuentasPorCobrar = [
        {
          idcuenta: 1,
          balance: 100,
          estado: "Pendiente",
          codigoPaciente: "PAC-001"
        },
        {
          idcuenta: 2,
          balance: 200,
          estado: "Pendiente",
          codigoPaciente: "PAC-002"
        },
        {
          idcuenta: 3,
          balance: 300,
          estado: "Pendiente",
          codigoPaciente: "PAC-003"
        }
      ];

      const response = await api.get('api/CuentaCobrar/Get');
      if (response.data) {
        this.cuentasPorCobrar = response.data;
      }
    },
    async Guardar() {
      const response = await api[this.modoEdicion ? 'put' : 'post'](`api/CuentaCobrar/${this.modoEdicion ? `Update${this.model.idcuenta}` : 'Add'}`, this.model);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha guardado la cuenta por cobrar exitosamente");
      } else {
        console.error('Error al agregar la cuenta por cobrar:', response.status, response.statusText);
        push.warning("Los datos ingresados no son válidos");
      }
    },
    async Eliminar(idcuenta) {
      const response = await api.delete(`api/CuentaCobrar/Delete${idcuenta}`);
      if (response.status === 200) {
        const result = response.data;
        push.success("Se ha eliminado la cuenta por cobrar exitosamente");
      } else {
        console.error('Error al eliminar la cuenta por cobrar:', response.status, response.statusText);
        push.warning("Los datos ingresados no son válidos");
      }
    },
    ConfirmarEliminar(event, idcuenta) {
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
          await this.Eliminar(idcuenta);
        },
      })
    },
    Nuevo() {
      this.model = {
        idcuenta: 0,
        balance: 0,
        estado: "",
        codigoPaciente: ""
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