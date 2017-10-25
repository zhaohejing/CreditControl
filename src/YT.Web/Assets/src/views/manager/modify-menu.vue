<template>
    <Button @click="test">
        awdawdawdawd</Button>
</template>
<script>
import { modifyUser, getUserForEdit, getRoles } from 'api/manage';
export default {
    props: {
        user: {
            type: Number,
            default() {
                return null
            }
        }
    },
    data() {
        return {
            current: {
                user: {
                    id: this.user,
                    name: '',
                    userName: '',
                    phoneNumber: '',
                    password: '',
                    isActive: true
                },
                assignedRoleNames: [],
                setDefaultPassword: true
            },
            roles: []
         
        }
    },
    created() {
    },
    mounted() {

    },
    methods: {
        async init() {

        },
        commit() {
            getUserForEdit({ id: this.current.user.id }).then(c => {
                if (c.data.success) {
                    this.$emit('submit-complete', false);

                }
            })
        }
    }
}
</script>
