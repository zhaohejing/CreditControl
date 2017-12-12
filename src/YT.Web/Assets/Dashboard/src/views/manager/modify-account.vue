<template>
    <Tabs value="user">
        <TabPane label="用户信息" name="user">
            <Row>
                <Col :md="22">
                <Form ref="user" :model="current.user" :rules="ruleValidate" :label-width="120">
                    <FormItem label="用户名" prop="userName">
                        <Input v-model="current.user.userName" placeholder="请输入用户名"></Input>
                    </FormItem>
                    <FormItem label="姓名" prop="name">
                        <Input v-model="current.user.name" placeholder="请输入姓名"></Input>
                    </FormItem>
                    <FormItem label="手机" prop="phoneNumber">
                        <Input v-model="current.user.phoneNumber" placeholder="请输入手机"></Input>
                    </FormItem>
                    <FormItem label="是否启用">
                        <i-switch v-model="current.user.isActive">
                            <span slot="open">启用</span>
                            <span slot="close">禁用</span>
                        </i-switch>
                    </FormItem>
                    <FormItem label="是否默认密码">
                        <i-switch v-model="current.setDefaultPassword">
                            <span slot="open">是</span>
                            <span slot="close">否</span>
                        </i-switch>
                    </FormItem>
                    <FormItem v-if="!current.setDefaultPassword" label="密码">
                        <Input type="password" v-model="current.user.password" placeholder="请输入密码"></Input>
                    </FormItem>
                </Form>
                </Col>
            </Row>
        </TabPane>
        <TabPane label="角色信息" name="role">
            <Row>
                <Col :offset="2" :md="22">
                <CheckboxGroup v-model="current.assignedRoleNames">
                    <Checkbox v-for="role in roles" :label="role.roleName" :key="role.roleName">{{role.roleDisplayName}}</Checkbox>
                </CheckboxGroup>
                </Col>
            </Row>
        </TabPane>
    </Tabs>
</template>
<script>
import { modifyUser, getUserForEdit, getRoles } from 'api/manage';
import { allPermissions } from 'api/menu';
export default {
    nmae:'modifyAccount',
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
            roles: [],
            ruleValidate: {
                name: [
                    { required: true, message: '姓名不可为空', trigger: 'blur' }
                ],
                userName: [
                    { required: true, message: '用户名不可为空', trigger: 'blur' }
                ],
                phoneNumber: [
                    { required: false, message: '手机不合法', trigger: 'blur' }
                ]

            }
        }
    },
    created() {
        this.init();
    },
    mounted() {

    },
    methods: {
        async init() {
            getUserForEdit({ id: this.current.user.id }).then(c => {
                if (c.data.success) {
                    this.current.user = c.data.result.user;
                    this.roles = c.data.result.roles;
                    this.genderRoles();
                }
            })
        },
     
        commit() {
            this.$refs.user.validate((valid) => {
                if (valid) {
                    modifyUser(this.current).then((response) => {
                        if (response.data.success) {
                            this.$root.eventHub.$emit('account');
                        } else {
                            this.$root.eventHub.$emit('account');
                        }
                    }).catch(erroe => {
                         this.$Message.error(erroe.error);
                        this.$root.eventHub.$emit('account');
                    });

                } else {
                    this.$Message.error('表单验证失败!');
                    this.$root.eventHub.$emit('account');
                }
            })
        },
        genderRoles() {
            this.current.assignedRoleNames = [];
            if (this.roles) {
                this.roles.forEach(c => {
                    if (c.isAssigned) {
                        this.current.assignedRoleNames.push(c.roleName);
                    }
                })
            }
        }

    }
}
</script>
