<template>
  <div class="sidebar">
    <nav class="sidebar-nav">
      <sidebar-item :routes='menus'></sidebar-item>

    </nav>
  </div>
</template>
<script>
import SidebarItem from './SidebarItem';
import { mapGetters, mapActions } from 'vuex';
import { userMenus } from 'api/menu';
export default {
  name: 'sidebar',
  data() {
    return {
      menus: null
    }
  },
  created: function() {
    this.getUserMenus();
  },
  components: { SidebarItem },
  computed: {
  },
  methods: {
    handleClick(e) {
      e.preventDefault()
      e.target.parentElement.classList.toggle('open')
    },
    getUserMenus() {
      userMenus().then(r=>{
        if(r.data.success){
           /*转换树结构*/
                    this.menus = this.$converToTreedata(r.data.result.items,
                        null,
                        'parentId',null);
         
        }
      });

    }
  }
}
</script>

<style lang="css">
.nav-link {
  cursor: pointer;
}
</style>
