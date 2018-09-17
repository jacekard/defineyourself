from plotly.offline import download_plotlyjs, init_notebook_mode, plot, iplot
from plotly.graph_objs import Scatter, Layout, Figure


def get_data_from_file(file_name):
    file_data = []
    try:
        file = open(file_name, "r+")
        for line in file:
            file_data.append(float(line))
        file.close()
        return file_data
    except IOError:
        print("Could not open "+file_name)
        return False


def main():
    data_x = get_data_from_file("output_x.txt")
    data_y = get_data_from_file("output_y.txt")
    
    data_x2 = get_data_from_file("input_x.txt")
    data_y2 = get_data_from_file("input_y.txt")
    
    x_zero = get_data_from_file("markers_x.txt")
    y_zero = get_data_from_file("markers_y.txt")
    
    if not data_x or not data_y  or not data_y2:
        return
    
    x_axis = data_x

    # Create a trace
    trace0 = Scatter(
        x=data_x,
        mode='lines',
        y=data_y,
        name='interpolacja',
        line = dict(color = ('rgb(0, 0,255)'), width=2)
    )
    
    trace1 = Scatter(
        x=data_x2,
        y=data_y2,
        mode='lines',
        name='profil wysokościowy',
        line = dict(color = ('rgb(255,0,0)'), width=6)
    )
    
    trace2 = Scatter(
        x=x_zero,
        y=y_zero,
        mode='markers',
        name='punkty styczne',
        marker = dict(color = ('rgb(0,0,255)'), size=8)
    )

    layout = Layout(
        yaxis=dict(
            title='wysokość [m]',
            
        ),
        yaxis2=dict(
            title='',
            range=[-150, 300],
            side='right',
            overlaying='y',
            zeroline=False,
        ),
        xaxis=dict(
            type='int',
            title='dystans [m]',
        )
    )
   
    plot(Figure(data=[trace1,trace0,trace2], layout=layout))


if __name__ == "__main__":
    main()
