<template>
  <div class="h-full w-full content login-wrapper" v-if="$route.name == 'Login'">
      <router-view :key="$route.fullPath" />
  </div>
  <div class="content relative h-screen w-full layout-wrapper card-background lg:surface-ground" v-else>
    <SideBar />
    <NavBar />
    <main class="layout-content flex-1 flex flex-column lg:pb-2 lg:pt-2 lg:pr-2 pl-20 lg:h-full">
      <div class="card ground-background lg:border-1 border-none border-noround lg:border-round-xl flex flex-column flex-grow-1 p-3 lg:p-6 overflow-y-auto" >
        <router-view :key="$route.fullPath" />
      </div>
    </main>
  </div>
  <PageLoader />
  <BlockScreen v-if="$route.name !== 'Login'"/>
  <Notivue v-slot="item">
    <NotivueSwipe :item="item">
        <Notifications :item="item" :theme="($store.state.isDarkMode) ? darkTheme : pastelTheme" />
    </NotivueSwipe>
  </Notivue>
</template>

<script >
import { Notivue, Notifications, NotivueSwipe, pastelTheme, darkTheme } from 'notivue'
import SideBar from '@/layout/SideBar.vue'
import NavBar from '@/layout/NavBar.vue'
import PageLoader from "@/components/PageLoader.vue"
import BlockScreen from "@/components/BlockScreen.vue"

export default {
  name: 'App',
  components: {
    Notivue, 
    Notifications, 
    NotivueSwipe, 
    SideBar,
    NavBar,
    PageLoader,
    BlockScreen,
  },
  data() {
    return {
      pastelTheme,
      darkTheme
    }
  }
}
</script>


<style>
</style>
