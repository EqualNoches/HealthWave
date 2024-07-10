import { createStore } from 'vuex'

export default createStore({
  state: {
    loading: false,
    user: {},
    isMobile: false,
    screenLocked: false,
    isDarkMode: false,
  },
  mutations: {
    setLoading(state, loading) {
      state.loading = loading;
    },
    setScreenLocked(state, screenLocked) {
      state.screenLocked = screenLocked;
    },
    setUser(state, user) {
      state.user = user;
    },
    setIsMobile(state, isMobile) {
      state.isMobile = isMobile;
    },
    setIsDarkMode(state, isDarkMode) {
      state.isDarkMode = isDarkMode;
    }
  },
  actions: {
  },
  getters: {
    isLoading: state => state.loading,
    getUser: state => state.user,
    isMobile: state => state.isMobile,
    isScreenLocked: state => state.screenLocked,
    isDarkMode: state => state.isDarkMode
  }
});