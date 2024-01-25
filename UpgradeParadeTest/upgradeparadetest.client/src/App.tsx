
import { Route, Routes, BrowserRouter as Router } from 'react-router-dom';
import OrderForm from './OrderForm';
import MainPage from './MainPage';
import SuccessPayment from './SuccessPayment';


function App() {
    return (
        <Router>
            <Routes>
                <Route path='/' element={<MainPage />} />
                <Route path='/orderForm' element={<OrderForm />} />
                <Route path='/success' element={<SuccessPayment />} />
                <Route path='/cancel' element={<SuccessPayment />} />
            </Routes>
        </Router>
   )

}

export default App;