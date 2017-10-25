<template>
    <div>
        <Row>
            <Col :md="layout[0]">
            <slot name="search"></slot>
            </Col>
            <Col :md="layout[1]">
            <slot name="buttons">
                <Button type="primary" shape="circle" icon="ios-search" @click.native="initData">查询</Button>
            </slot>
            </Col>
            <Col :md="layout[2]">
            <slot name="actions"></slot>
            </Col>
        </Row>
        <Table   highlight-row ref="list" :stripe="stripe" :show-header="showHeader" :columns="columns" :data="items" :no-data-text="emptyContent" @on-select="selectOne" @on-select-cancel="cancelSelect" @on-current-change="changeCurrent" @on-select-all="selectAll">
        </Table>
        <Page :total="total" :current="pageModel.current" show-total show-sizer show-elevator @on-change="pageChange" @on-page-size-change="pageSizeChange" style="text-align:right;margin-top:50px">
        </Page>
    </div>
</template>

<script>

export default {
    name: 'milk-table',
    props: {
        //斑马纹
        stripe: {
            type: Boolean,
            required: false,
            default: false
        },
        //表格头字段
        columns: {
            type: Array,
            required: true
        },
        //是否显示表头
        showHeader: {
            type: Boolean,
            required: false,
            default: true
        },

        /*是否分页，默认分页*/
        pagination: {
            type: Boolean,
            default: true,
            required: false
        },
        layout: {
            type: Array,
            default: () => [16, 2, 6],
            required: false
        },
        /*查询Api,方法*/
        searchApi: {
            type: Function,
            required: true
        },
        //参数
        params: {
            type: Object,
            default() {
                return {};
            },
            required: false
        }

    },
    data() {
        return {
            items: [],
            total: 0,
            selects: [],
            current: {},
            emptyContent: '暂无数据',
            pageModel: {
                current: 1,
                maxResultCount: 10
            }
        }

    },
    created() {
        this.initData();
    },
    methods: {
        //初始化
        async initData() {
            try {
                this.getApiData();
            } catch (err) {
                this.$Message.error('对方不想说话，并且向你抛出了一个异常');
            }
        },
        /*获取数据*/
        getApiData() {
            var params = this.params;
            params.skipCount = (this.pageModel.current - 1) * this.pageModel.maxResultCount;
            params.maxResultCount = this.pageModel.maxResultCount;
            this.items = [];
            this.searchApi(params).then(response => {
                var result = response.data;
                this.items = (result && result.result && result.result.items) || [];
                this.total = (result && result.result && result.result.totalCount) || 0;
            });
        },
        selectOne(selection, row) {
            this.selects = selection;
        },
        changeCurrent(currentRow, oldCurrentRow) {
            this.current = currentRow;
        },
        cancelSelect(selection, row) {
            this.selects = selection;
        },
        selectAll(all, sels) {
            this.selects = all;
        },
        pageChange(current) {
            this.pageModel.current = current;
            this.getApiData();
        },
        pageSizeChange(size) {
            this.pageModel.maxResultCount = size;
            this.getApiData();
        }
    },
    computed() {

    }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.wscn-icon {
    margin-right: 10px;
}

.hideSidebar .menu-indent {
    display: block;
    text-indent: 10px;
}

</style>

