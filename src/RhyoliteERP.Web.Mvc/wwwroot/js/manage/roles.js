Vue.use(window.vuelidate.default);
const { required, minValue } = window.validators;

var vue = new Vue({
    el: '#app',
    data: {

        isShimmerLoading: false,
        isLoading: false,
        modalTitle: 'Add Role',
        payload: {},
        userList: [],
        roleList: [],
        permissionList: [],
        id: null,
        tenantId: null,
        name: '',
        displayName: '',
        grantedPermissions: [],
        roles: [],
        pageNo: 1,
        pageSize: 10,
        totalCount: 0,
        totalPages: 0,
        lowerBound: 0,
        upperBound: 0
       
    },
    validations() {

        return {

            name: {
                required
            },
            
            
        }


    },
    methods: {

     
        addRole() {

            this.$v.$touch();

            if (this.$v.$invalid) {

                for (let key in Object.keys(this.$v)) {

                    const input = Object.keys(this.$v)[key];

                    if (this.$v[input].$error) {

                        this.$refs[input].focus();

                        break;
                    }

                }

                return;
            }



            this.payload = {};
            this.isLoading = true;
            
            this.payload.name = this.name;
            this.payload.displayName = this.displayName;
          
            this.payload.roleNames = [this.selectedRole];

            axios.post('/Manage/AddRole', this.payload).then((response) => {

                this.isLoading = false;
                this.toastHandler('', 'Role created.');
                this.getRoles();
                $("#role-modal").modal('hide');
                this.modalTitle = 'Add Role';

            }).catch((error) => {

                this.isLoading = false;
                this.toastHandler('error', 'Unable to create role.');
                $("#role-modal").modal('hide');

            });

        },
        selectPermission(evt, permission, index) {

            if (evt.target.checked) {

                this.grantedPermissions.push(permission.name);

                var permissionRecord = this.permissionList[index];

                permissionRecord.isSelected = true;
                this.permissionList[index] = permissionRecord;



            }
            else {

                const index = this.grantedPermissions.findIndex(item => item === permission.name);

                this.grantedPermissions.splice(index, 1);

            }
        },

        deleteRole(role) {

            axios.get('/Manage/DelRole?id=' + role.id).then((response) => {

                this.toastHandler('', 'Role deleted.');
                this.getRoles();


            }).catch((error) => {

                this.toastHandler('error', 'Unable to delete role.');

                console.log(error);
            });
        },

        viewRoleDetails(roleInfo) {

            console.log(roleInfo);
            this.modalTitle = 'Role Details';
            this.name = roleInfo.name;
            this.displayName = roleInfo.displayName;
            
            $("#role-modal").modal('show');
        },

        resetModal() {

            this.modalTitle = 'Add Role';
            this.name = '';
            this.displayName = '';

        },
        getRoles() {

            axios.get(`/Manage/GetRoles?pageNo=${this.pageNo}&pageSize=${this.pageSize}`).then((response) => {

                this.roleList = response.data.data;
                this.totalCount = response.data.totalCount;
                this.totalPages = response.data.totalPages;

                this.lowerBound = response.data.lowerBound;
                this.upperBound = response.data.upperBound;

            }).catch((error) => {

                console.log(error);
            });
        },
        
        getPermissions() {

            axios.get('/Manage/GetPermissions').then((response) => {

                this.permissionList = response.data;

            }).catch((error) => {

                console.log(error);
            });
        },
        getPermissionName(catId) {

            var parentId;
            var returnName;
            this.permissionList.forEach(item => {
                if (item.id == catId) {
                    parentId = item.parentId;
                    returnName = item.name;
                }
                this.permissionList.forEach(cat => {
                    if (cat.id == parentId) {
                        return (`${getPermissionName(parentId)} / ${returnName}`);
                    }
                })
            })
            this.categoryName.push(returnName);
        },
        toastHandler(type, message = 'An unexpected error occurred.') {

            //display alert with 
            toastr.options = {
                closeButton: true,
                progressBar: true,
                showMethod: 'fadeIn',
                hideMethod: 'fadeOut',
                timeOut: 10000,
            };

            switch (type) {

                case 'warning':
                    toastr.warning('', message);
                    break;
                case 'error':
                    toastr.error('', message);
                    break;
                default:
                    toastr.success('', message);
                    break;
            }

        },

        
    },
    computed: {

        permissionTree() {

            return this.permissionList;
        }
      
    },
    watch: {

       

    },

    created() {
         
        this.getRoles();
        this.getPermissions();
    },
});