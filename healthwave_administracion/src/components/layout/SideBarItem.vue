<template>
  <span class="relative" >
    <span v-if="$route.path == item.path"
      style="transform: none; transform-origin: 50% 50% 0px; border-radius: 9999px; width: .125rem; left: -1rem; top: .5rem; bottom: .5rem; position: absolute; background: var(--p-button-secondary-color); margin: 3px 0px"></span>
    <router-link :to="item.path" class="w-full" style="text-decoration: none; color: inherit;">
      <Button
        :class="`${RutaActiva() ? 'font-medium' : 'font-medium text-muted-color '} flex items-center w-full mb-1`"
        :severity="RutaActiva() ? 'primary' : 'secondary' " :text="!(RutaActiva())">
        <span :class="`${item.icon} mr-2`" />
        <span>{{ item.label }}</span>
        <Badge v-if="item.badge" class="ml-auto" :value="item.badge" />
        <span v-if="item.shortcut"
          class="ml-auto rounded bg-emphasis text-muted-color text-sm p-1">{{ item.shortcut
          }}</span>
      </Button>
    </router-link>
  </span>
</template>

<script>
export default {
  name: "SideBarItem",
  created() {},
  data() {
    return {};
  },
  props: {
    item: Object,
  },
  methods: {
    RutaActiva() {
      return this.$route.path == this.item.path;
    }
  },
};
</script>