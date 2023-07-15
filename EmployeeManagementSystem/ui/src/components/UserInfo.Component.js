import React, { useState,useEffect,useContext } from 'react';
import { Button, Modal, Form,Row,Col } from 'react-bootstrap';
import { UserContext } from '../contexts/UserContext';
import {BsFillFileEarmarkTextFill} from  'react-icons/bs'

const UserDetailModal = ({ showdetailModal, setDetailsShowModal }) => {
    
const {userData}=useContext(UserContext);

  
  const handleClose = () => {
    setDetailsShowModal(false);
  };

  return (
    <>
     
      <Modal show={showdetailModal} onHide={handleClose} size="lg">
        <Modal.Header closeButton>
          <Modal.Title>User Info</Modal.Title>
        </Modal.Header>

        <Modal.Body>
            <Row>
             <Col>
            <Form.Group controlId="controlname">
              <Form.Label>User Name</Form.Label>
              <input className='form-control' value={userData?.name} readOnly/> 
             
            </Form.Group>
            </Col>
            <Col>
            <Form.Group controlId="mobilecontrol">
              <Form.Label>Mobile No</Form.Label>
              <input className='form-control' value={userData?.mobileNo} readOnly/>
             
            </Form.Group>
            </Col>
            </Row>
        
            <Row>
             <Col>
            <Form.Group controlId="controlemail">
              <Form.Label>Email Id</Form.Label>
              <input className='form-control' value={userData?.email} readOnly/>
             
            </Form.Group>
            </Col>
            <Col>
            <Form.Group controlId="controlemployeeType">
              <Form.Label>Employee Type</Form.Label>
              <input className='form-control' value={userData?.employeeType} readOnly/>
            </Form.Group>
            </Col>
            </Row>
            <Row>
            <Col>
            <Form.Group controlId="controldesignation">
              <Form.Label>Designation</Form.Label>
              <input className='form-control' value={userData?.designation} readOnly/>
              
            </Form.Group>
            </Col>
            <Col>
            <Form.Group controlId="controllocationId">
              <Form.Label>Location</Form.Label>
              <input className='form-control' value={userData?.locationName} readOnly/>

            </Form.Group>
            </Col>
            </Row>

            <Row>
            <Col>
            <Form.Group controlId="controlnationality">
              <Form.Label>Nationality</Form.Label>
              <input className='form-control' value={userData?.nationality}  readOnly/>
             
             
            </Form.Group>
            </Col>
            <Col>
            <Form.Group controlId="controlpassportNo">
              <Form.Label>Passport No</Form.Label>
              <input type='text' className='form-control' readOnly
                value={userData?.passportNo==null ?"":userData?.passportNo} ></input>
               
            </Form.Group>
            </Col>
            </Row>
            <Row>
               
                <Col>
                <Form.Group controlId="controlpassportExpireDate">
                    <Form.Label>Passport Expire Date</Form.Label>
                    <input type="Date" className='form-control' readOnly
                     value={userData.passportExpireDate}></input>
                </Form.Group>
                </Col> 
            </Row>
            
           
           
            <Row className='my-3'>
              <Col className='px-3'>

              <div className='files'>
          {
            userData?.passportFile &&(
              <div className='file-Icon'>
                <div>Passport</div>
                <BsFillFileEarmarkTextFill />
                <div>{userData?.passportFile?.split('/')[3]}</div>
              </div>
            )
          }
          {
            userData?.emirateIdFile &&(
              <div className='file-Icon'>
                <div> Emirate Id </div>
                <BsFillFileEarmarkTextFill />
                <div>{userData?.emirateIdFile?.split('/')[3]}</div>
              </div>
            )
          }
          {
            userData?.drivingLicenceFile &&(
              <div className='file-Icon'>
                <div> Driving Licence </div>
                <BsFillFileEarmarkTextFill style={{width:"64px"}}/>
                <div>{userData?.drivingLicenceFile?.split('/')[3]}</div>
              </div>
            )
          }
          {
            userData?.personPhoto &&(
              <div className='file-Icon'>
                <div> Person Photo </div>
                <BsFillFileEarmarkTextFill />
                <div>{userData?.personPhoto?.split('/')[3]}</div>
              </div>
            )
          }
              </div>

              </Col>
            </Row>
        </Modal.Body>

        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
      
    </>
  );
};

export default UserDetailModal;
