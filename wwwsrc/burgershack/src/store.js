  import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios';

let server = Axios.create({
  baseURL: "//localhost:5000/api/",
  timeout: 3000
})

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    burgers: {}
  },
  mutations: {
    setBurgers(state, burgers) {
      for (let i = 0; i < burgers.length; i++) {
        let burger = burgers[i]
        Vue.set(state.burgers, burger.id, burger)
      }
    }
  },
  actions: {
    getAllBurgers({ dispatch, commit }) {
      server.get("burgers")
        .then(res => {
          commit('setBurgers', res.data)
        })
    }
  }
})
