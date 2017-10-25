<template>
  <div class="app">
    <AppHeader/>
    <div class="app-body">
      <Sidebar/>
      <main class="main">
        <breadcrumb :list="list" />
        <div class="container-fluid">
          <router-view></router-view>
        </div>
      </main>
      <AppAside/>
    </div>
    <AppFooter/>
  </div>
</template>

<script>
import AppHeader from '../components/Header'
import Sidebar from '../components/Sidebar'
import AppAside from '../components/Aside'
import AppFooter from '../components/Footer'
import Breadcrumb from '../components/Breadcrumb'
import { mapGetters, mapActions } from 'vuex';
export default {
  name: 'full',
  components: {
    AppHeader,
    Sidebar,
    AppAside,
    AppFooter,
    Breadcrumb
  },
  created() {
    this.GetInfo();
  },
  computed: {
    ...mapGetters([
      'name'
    ]),
    name() {
      return this.$route.name
    },
    list() {
      return this.$route.matched
    }
  },
  methods: {
    ...mapActions([
      'GetInfo'
    ])
  }
  //,
  // watch: {
  //   name: function(newvalue) {
  //     debugger;
  //     if (newValue && newValue!=='') {
	// 			}else{
	// 			 this.$Message.error('请重新登陆');
	// 				setTimeout(() => {
	// 					this.$router.replace('/login')
	// 				}, 500)
	// 			}
  //   }
  // }
}
</script>
