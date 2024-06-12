/* eslint-disable react-hooks/exhaustive-deps */
import { useEffect, useState } from 'react'
import './App.css'
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Button } from 'primereact/button';

function App() {
  const [inputValue, setInputValue] = useState(0)
  const [data, setData] = useState([])
  const [index, setIndex] = useState(0)
  useEffect(() => {
    fetch('http://localhost:3000').then(res => res.json()).then(r => setData(r))
  }, []);
  function add(){
    fetch(`http://localhost:3000/add/${inputValue}`, {
      method: 'PUT'
    })
    .then(res => res.json())
    .then(r => setData(r))
  }
  function edit(){
    fetch(`http://localhost:3000/edit/${index}/${inputValue}`, {
      method: 'PUT'
    })
    .then(res => res.json())
    .then(r => setData(r))
    setShowModal(false)
  }
  function del(){
    fetch(`http://localhost:3000/delete/${index}`, {
      method: 'DELETE'
    })
    .then(res => res.json())
    .then(r => setData(r))
  }
  const [showModal, setShowModal] = useState(false)
  return (
    <>
      <DataTable value={data}>
        <Column field="id" sortable header="ID"></Column>
        <Column field="value" sortable header="Value"></Column>
        <Column body={(rowData) => <Button onClick={() => {setIndex(rowData.id); setShowModal(true)}}>Edit</Button>}></Column>
        <Column body={(rowData) => <Button onClick={() => del(rowData.id)}>Delete</Button>}></Column>
      </DataTable>
      <input type="text" onChange={(ev) => setInputValue(ev.target.value)}/>
      <button onClick={() => add()}>add</button>
      {showModal ? <div className='modal'>
        <div className='modal-content'>
          <h1>Index: {index}</h1>
          <input type="text" onChange={(ev) => setInputValue(ev.target.value)}/>
          <button onClick={() => edit(index)}>Edit</button>
          <button onClick={() => setShowModal(false)}>Close</button>
        </div>  
      </div> : null}
    </>
  )
}

export default App
