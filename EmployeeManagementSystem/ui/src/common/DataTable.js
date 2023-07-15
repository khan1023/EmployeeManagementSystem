import React, { useState } from 'react';
import{BsFillPencilFill,BsFillTrashFill,BsInfoLg}from 'react-icons/bs'

const DataTable = ({ data, columns, actions }) => {
  const itemsPerPage = 10    ; // Number of items to display per page
  const [currentPage, setCurrentPage] = useState(1);

  // Calculate total number of pages based on the data length and items per page
  const totalPages = Math.ceil(data?.length / itemsPerPage);

  // Get the slice of data for the current page
  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;
  const currentData = data?.slice(startIndex, endIndex);

  const handleAction = (action, itemId) => {
    if (typeof actions[action] === 'function') {
      actions[action](itemId);
    }
  };

  return (
    <div>
      <table className='table border'>
        <thead>
          <tr>
            {columns.map((column) => (
              <th key={column.key}>{column.title}</th>
            ))}
            <th>Actions
            <p style={{width:"150px",margin:"0px"}}></p>
            </th>
          </tr>
        </thead>
        <tbody>
          {
            currentData&&currentData?.length>0&&
          currentData?.map((item) => (
            <tr key={item.id}>
              {columns.map((column) => (
                <td key={column.key}>{item[column.key]}</td>
              ))}
              <td>
              {actions.view && (
                  <button className='btn btn-info mx-1' onClick={() => handleAction('view', item)}>
                    <BsInfoLg />
                  </button>
                )}
                {actions.edit && (
                  <button className='btn btn-primary mx-1' onClick={() => handleAction('edit', item)}>
                    <BsFillPencilFill />
                  </button>
                )}
                {actions.delete && (
                  <button className='btn btn-danger mx-1' onClick={() => handleAction('delete', item.id)}>
                    <BsFillTrashFill />
                  </button>
                )}
              </td>
            </tr>
          ))
          
          }
          {
            (!currentData || currentData?.length<1)?(
                <tr>
                    <td className='text-center' colSpan={columns.length}>No Records Found</td>
                </tr>
            ):<tr></tr>
          }
        </tbody>
      </table>

      {/* Pagination */}
      <ul className='pagination justify-content-end'>
        <li className='page-item'
          onClick={() => setCurrentPage((prevPage) =>prevPage<2?1: prevPage - 1)}
          disabled={currentPage === 1}
        >
          <a className='page-link'>Previous</a>
        </li>
        <li className='page-item'> <a className='page-link'>{currentPage}</a></li>
        <li className='page-item'
          onClick={() => setCurrentPage((prevPage) => totalPages== prevPage?prevPage:prevPage + 1)}
          disabled={currentPage === totalPages}
        >
          <a className='page-link'>Next</a>
        </li>
      </ul>
    </div>
  );
};

export default DataTable;
