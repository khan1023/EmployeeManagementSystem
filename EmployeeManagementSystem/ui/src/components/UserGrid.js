import React, { useState, useEffect,useContext } from 'react';
import { Table,Button } from 'react-bootstrap';
import {getUserList,deleteData} from '../service/userService'
import AddUserModal from '../components/CreateUser.Component'
import{BsFillPencilFill,BsFillTrashFill,BsInfoLg}from 'react-icons/bs'
import { UserContext } from '../contexts/UserContext';
import EditUserModal from './EditUser.Component';
import UserDetailModal from './UserInfo.Component'
import swal from "sweetalert";
import DataTable from '../common/DataTable'
export const UserGrid = () => {

    
    const [showModal, setShowModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDetailModal, setShowDetailModal] = useState(false);
    const [selectedUserId, setSelectedUserId] = useState(0);
    const{userList,setUserlist,setUser} = useContext(UserContext);
    const[isEdit,setIsEdit]=useState(false);
    const[isUserDetail,setIsUserDetail]=useState(false);
    // Fetch data from API
    useEffect(() => {
        fetchUserList();
    }, []);

    const fetchUserList = async () => {
       
        const Data = await getUserList();
        console.log("Data",Data);
        Data.map(val=>{
          if(val.passportExpireDate !==null && val.passportExpireDate !==""){
            val.passportExpireDate=val.passportExpireDate.replaceAll('/','-');
            let d = val.passportExpireDate.split('-');
            val.passportExpireDate=`${d[2]}-${d[1]}-${d[0]}`
          }
        }
            )
          setUserlist(Data);
      };

      const handleAddUserClick = () => {
        setIsUserDetail(false);
        setShowDetailModal(false);
        setIsEdit(false);
        setShowModal(true);
      };

      const handleEditClick = (user) => {
        setUser({...user})
            setIsEdit(true);
            setShowEditModal(true);
      };
      const handleDetaliClick = (user) => {
        setUser({...user})
            setIsUserDetail(true);
            setShowDetailModal(true);
      };
      const handleDeleteClick = (id) => {

        swal({
          title: "Are you sure?",
          text: "You will not be able to recover data!",
          icon: "warning",
          buttons: true,
          dangerMode: true,
        })
        .then((willDelete) => {
          if (willDelete) {
            deleteData(id,fetchUserList);
          } 
        });
      };

   const cols=[
    {key:"name", title:"Name"},
    {key:"mobileNo", title:"MobileNo"},
    {key:"email", title:"Email Id"},
    {key:"employeeType", title:"Employee Type"},
    {key:"designation", title:"Designation"},
    {key:"locationName", title:"LocationName"},
    {key:"nationality", title:"Nationality"}
];
    return (
      <div className='container'>
      
        <section className='py-5' style={{margin:"5px"}}><h1 id="tableLabel">User List <div style={{float:"right",margin:"5px"}}>
            <Button variant="primary" onClick={handleAddUserClick}>Add User</Button></div></h1>

            {isUserDetail?<UserDetailModal showdetailModal={showDetailModal} setDetailsShowModal={setShowDetailModal}/>:""}

        {isEdit? <EditUserModal showModal={showEditModal} setShowModal={setShowEditModal} refreshTable={()=>fetchUserList()} ></EditUserModal>
        :
        <AddUserModal showModal={showModal} setShowModal={setShowModal} refreshTable={()=>fetchUserList()} />}
        <DataTable data={userList} columns={cols} actions={{edit: handleEditClick, delete: handleDeleteClick,view:handleDetaliClick}}>

        </DataTable>
        </section>
        </div>
        );
  }
 export default UserGrid;